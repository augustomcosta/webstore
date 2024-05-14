import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RouterLink } from '@angular/router';
import { IPaymentMethod } from '../../../../core/models/payment-method';
import { PaymentMethodService } from '../../../../services/payment-method.service';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import * as CheckoutActions from '../../data/checkout.actions';
import * as PaymentActions from './data/payment.actions';
import { select, Store } from '@ngrx/store';
import { CreditCardFormComponent } from './payment-form-options/credit-card-form/credit-card-form.component';
import { PaypalFormComponent } from './payment-form-options/paypal-form/paypal-form.component';
import { PixFormComponent } from './payment-form-options/pix-form/pix-form.component';
import { PaymentSelectorComponent } from './payment-selector/payment-selector.component';
import { selectFormData } from '../data/shipping/shipping.selectors';
import { selectPaymentData } from './data/payment.selectors';

@Component({
  selector: 'app-payment-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink,
    AsyncPipe,
    CreditCardFormComponent,
    PaypalFormComponent,
    PixFormComponent,
    PaymentSelectorComponent,
  ],
  templateUrl: './payment-form.component.html',
  styleUrl: './payment-form.component.css',
})
export class PaymentFormComponent implements OnInit {
  paymentMethodService = inject(PaymentMethodService);
  paymentMethods$: Observable<IPaymentMethod[]> | undefined;
  paymentForm!: FormGroup;
  fb = inject(FormBuilder);
  store = inject(Store);
  isFormValid = false;

  updateOverallFormValidity(isValid: boolean) {
    this.isFormValid = isValid;
  }

  selectPaymentMethod(payment: string) {
    this.paymentForm.patchValue({
      paymentMethod: payment,
    });
    this.store.dispatch(PaymentActions.submitPayment({ payment }));
  }

  constructor() {
    this.paymentMethods$ = this.paymentMethodService.paymentMethods$;
    this.paymentForm = this.fb.group({
      paymentMethod: ['', Validators.required],
    });
  }

  goToNextStep() {
    this.store.dispatch(CheckoutActions.nextStep());
  }

  goToPreviousStep() {
    this.store.dispatch(CheckoutActions.previousStep());
  }

  ngOnInit(): void {}
}
