import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RouterLink } from '@angular/router';
import { IPaymentMethod } from '../../../domain/models/payment-method';
import { PaymentMethodService } from '../../../data/services/payment-method.service';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import * as CheckoutActions from '../../state/checkout/checkout.actions';
import * as PaymentActions from '../../state/payment/payment.actions';
import { Store } from '@ngrx/store';
import { CreditCardFormComponent } from '../credit-card-form/credit-card-form.component';
import { PixFormComponent } from '../pix-form/pix-form.component';
import { PaymentSelectorComponent } from '../payment-selector/payment-selector.component';

@Component({
  selector: 'app-payment-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink,
    AsyncPipe,
    CreditCardFormComponent,
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

  constructor() {
    this.paymentMethods$ = this.paymentMethodService.paymentMethods$;
    this.paymentForm = this.fb.group({
      paymentMethod: ['', Validators.required],
    });
  }

  updateOverallFormValidity(isValid: boolean) {
    this.isFormValid = isValid;
  }

  selectPaymentMethod(payment: string) {
    this.paymentForm.patchValue({
      paymentMethod: payment,
    });
    this.store.dispatch(PaymentActions.submitPayment({ payment }));
  }

  goToNextStep() {
    this.store.dispatch(CheckoutActions.nextStep());
  }

  goToPreviousStep() {
    this.store.dispatch(CheckoutActions.previousStep());
  }

  ngOnInit(): void {}
}
