import { Component, inject, OnInit } from '@angular/core';
import { BasketService } from '../../../../services/basket.service';
import { catchError, of } from 'rxjs';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { select, Store } from '@ngrx/store';
import { UserService } from '../../../../services/user.service';
import { AddressVO } from '../../../../core/models/address-vo';
import { CepResponse } from '../../../../interfaces/cep-response';
import { submitForm } from '../data/shipping/shipping.actions';
import * as CheckoutActions from '../../data/checkout.actions';
import { selectFormData } from '../data/shipping/shipping.selectors';

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

  constructor() {
    this.address = new AddressVO();

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
    this.address.number = this.shippingForm.get('number')!.value;

    return this.userService
      .updateUserAddress(localStorage.getItem('userId')!, this.address)
      .subscribe();
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
}
