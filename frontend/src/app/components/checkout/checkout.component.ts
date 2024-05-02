import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { BasketService } from '../../services/basket.service';
import { IBasket, IBasketTotals } from '../../core/models/basket';
import { catchError, Observable, of, Subscription } from 'rxjs';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { CepResponse } from '../../interfaces/cep-response';
import { UserService } from '../../services/user.service';
import { AddressVO } from '../../core/models/address-vo';
import { select, Store } from '@ngrx/store';
import { selectStepperStep } from './data/checkout.selectors';
import * as CheckoutActions from './data/checkout.actions';
import { nextStep } from './data/checkout.actions';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [
    RouterLink,
    FormsModule,
    ReactiveFormsModule,
    AsyncPipe,
    CurrencyPipe,
  ],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css',
})
export class CheckoutComponent implements OnInit, OnDestroy {
  basketService = inject(BasketService);
  basket$: Observable<IBasket> | undefined;
  fb = inject(FormBuilder);
  form!: FormGroup;
  basket: IBasket | undefined;
  basketTotal$: Observable<IBasketTotals> | undefined;
  http = inject(HttpClient);
  store = inject(Store);
  userService = inject(UserService);
  address!: AddressVO;
  userAddress$: Observable<AddressVO> | undefined;
  userAddressSubscription: Subscription | undefined;
  private stepperStepSubscription: Subscription | undefined;

  stepperStep$: Observable<number>;

  constructor() {
    this.stepperStep$ = this.store.pipe(select(selectStepperStep));

    this.basket$ = this.basketService.basket$;
    this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });
    this.basketTotal$ = this.basketService.basketTotal$;
    this.address = new AddressVO();
  }

  searchCep(cep: string): void {
    const validCep = /^[0-9]{8}$/;
    const onlyNumbers = /^[0-9]+$/;
    let err: string;

    this.http
      .get<CepResponse>(`https://viacep.com.br/ws/${cep}/json/`)
      .pipe(
        catchError(() => {
          if (!onlyNumbers.test(cep) || !validCep.test(cep)) {
            err = 'CEP inválido.';
            return of(null);
          }

          err = 'Erro na requisição. Não foi possível.';

          return of(null);
        }),
      )
      .subscribe((response) => {
        if (response) {
          this.fillAddressFields(response);
        } else {
          this.handleError(err);
        }
      });
  }

  handleError(error: string): void {
    const message = document.querySelector('#message') as HTMLInputElement;
    message.textContent = error || 'Erro ao buscar o CEP';
  }

  fillAddressFields(response: CepResponse): void {
    this.updateFormValues(response);

    this.updateAddressEntity(response);
  }

  onCepBlur(event: any): void {
    const cep = event.target.value;
    this.searchCep(cep);
  }

  updateUserAddress() {
    if (this.userAddressSubscription) {
      this.userAddressSubscription?.unsubscribe();
    }

    console.log('chegou aqui');
    this.address.number = this.form.get('number')!.value;

    this.userAddress$ = this.userService.updateUserAddress(
      localStorage.getItem('userId')!,
      this.address,
    );

    this.userAddressSubscription = this.userAddress$.subscribe();
  }

  ngOnDestroy() {
    this.userAddressSubscription?.unsubscribe();
    this.stepperStepSubscription?.unsubscribe();
  }

  private updateAddressEntity(response: CepResponse) {
    this.address.state = response.uf;
    this.address.city = response.localidade;
    this.address.street = response.logradouro;
    this.address.neighborhood = response.bairro;
    this.address.zipCode = response.cep;
  }

  private updateFormValues(response: CepResponse) {
    this.form.patchValue({
      street: response.logradouro,
      neighborhood: response.bairro,
      city: response.localidade,
      state: response.uf,
      cep: response.cep.replace('-', ''),
    });
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      street: [this.address.street, Validators.required],
      neighborhood: [this.address.neighborhood, Validators.required],
      city: [this.address.city, Validators.required],
      state: [this.address.state, Validators.required],
      cep: [this.address.zipCode, Validators.required],
      number: [this.address.number, Validators.required],
    });
  }

  goToNextStep() {
    this.store.dispatch(CheckoutActions.nextStep());
  }

  goToPreviousStep() {
    this.store.dispatch(CheckoutActions.previousStep());
  }
}
