import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { BasketService } from '../../services/basket.service';
import { IBasket, IBasketTotals } from '../../core/models/basket';
import { Observable } from 'rxjs';
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
export class CheckoutComponent implements OnInit {
  basketService = inject(BasketService);
  basket$: Observable<IBasket> | undefined;
  fb = inject(FormBuilder);
  form!: FormGroup;
  basket: IBasket | undefined;
  basketTotal$: Observable<IBasketTotals> | undefined;
  http = inject(HttpClient);
  userService = inject(UserService);
  address!: AddressVO;

  constructor() {
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

    if (!onlyNumbers.test(cep) || !validCep.test(cep)) {
      throw new Error('Cep inválido');
    }

    this.http
      .get<CepResponse>(`https://viacep.com.br/ws/${cep}/json/`)
      .subscribe(
        (response) => {
          this.fillAddressFields(response);
        },
        (error) => {
          this.handleError(error);
        },
      );
  }

  handleError(error: any): void {
    const message = document.querySelector('#message') as HTMLInputElement;
    message.textContent = error.message || 'Erro ao buscar o CEP';
    setTimeout(() => {
      message.textContent = '';
    }, 5000);
  }

  fillAddressFields(response: CepResponse): void {
    const cep = document.querySelector('#cep') as HTMLInputElement;
    const street = document.querySelector('#street') as HTMLInputElement;
    const neighborhood = document.querySelector(
      '#neighborhood',
    ) as HTMLInputElement;
    const city = document.querySelector('#city') as HTMLInputElement;
    const state = document.querySelector('#state') as HTMLInputElement;

    street.value = response.logradouro;
    neighborhood.value = response.bairro;
    city.value = response.localidade;
    state.value = response.uf;
    cep.value = response.cep.replace('-', '');

    this.address.state = response.uf;
    this.address.street = response.logradouro;
    this.address.neighborhood = response.bairro;
    this.address.zipCode = cep.value;

    this.form.get('street')!.markAsTouched();
    this.form.get('neighborhood')!.markAsTouched();
    this.form.get('city')!.markAsTouched();
    this.form.get('state')!.markAsTouched();
  }

  onCepBlur(event: any): void {
    const cep = event.target.value;
    this.searchCep(cep);
  }

  updateUserAddress() {
    this.userService.updateUserAddress(
      localStorage.getItem('userId')!,
      this.address,
    );
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      cep: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required],
      neighborhood: ['', Validators.required],
      street: ['', Validators.required],
      number: ['', Validators.required],
    });
  }
}
