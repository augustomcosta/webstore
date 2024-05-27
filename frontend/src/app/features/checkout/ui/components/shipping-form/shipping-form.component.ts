import { Component, inject, OnInit } from '@angular/core';
import { BasketService } from '../../../../basket/data/services/basket.service';
import { catchError, Observable, of } from 'rxjs';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { select, Store } from '@ngrx/store';
import { UserService } from '../../../../auth/data/services/user.service';
import { AddressVO } from '../../../../shared/value-objects/address-vo';
import { CepResponse } from '../../../../shared/interfaces/cep-response';
import { submitForm } from '../../state/shipping/shipping.actions';
import * as CheckoutActions from '../../state/checkout/checkout.actions';
import { selectFormData } from '../../state/shipping/shipping.selectors';
import { selectStepperStep } from '../../state/checkout/checkout.selectors';

@Component({
  selector: 'app-shipping-form',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './shipping-form.component.html',
  styleUrl: './shipping-form.component.css',
})
export class ShippingFormComponent implements OnInit {
  basketService = inject(BasketService);
  fb = inject(FormBuilder);
  shippingForm!: FormGroup;
  http = inject(HttpClient);
  userService = inject(UserService);
  address!: AddressVO;
  store = inject(Store);
  stepperStep$: Observable<number>;
  stepperStep!: number;

  constructor() {
    this.address = new AddressVO();

    this.stepperStep$ = this.store.pipe(select(selectStepperStep));

    this.shippingForm = this.fb.group({
      street: [this.address.street, Validators.required],
      neighborhood: [this.address.neighborhood, Validators.required],
      city: [this.address.city, Validators.required],
      state: [this.address.state, Validators.required],
      cep: [this.address.zipCode, Validators.required],
      number: [this.address.number, Validators.required],
    });
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
            err = 'Invalid CEP';
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
    message.textContent = error || 'Error searching CEP';
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
    this.address.number = this.shippingForm.get('number')!.value;

    return this.userService.updateUserAddress(this.address);
  }

  goToNextStep() {
    this.store.dispatch(CheckoutActions.nextStep());
    this.store.dispatch(submitForm());
  }

  ngOnInit(): void {
    this.store.pipe(select(selectFormData)).subscribe((address) => {
      if (address) {
        this.shippingForm.patchValue({
          street: address.street,
          neighborhood: address.neighborhood,
          city: address.city,
          state: address.state,
          cep: address.zipCode,
          number: address.number,
        });
      }
    });
  }

  private updateAddressEntity(response: CepResponse) {
    this.address.state = response.uf;
    this.address.city = response.localidade;
    this.address.street = response.logradouro;
    this.address.neighborhood = response.bairro;
    this.address.zipCode = response.cep.replace('-', '');
  }

  private updateFormValues(response: CepResponse) {
    this.shippingForm.patchValue({
      street: response.logradouro,
      neighborhood: response.bairro,
      city: response.localidade,
      state: response.uf,
      cep: response.cep.replace('-', ''),
    });
  }
}
