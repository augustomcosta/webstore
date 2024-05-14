import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { CreditCardFormComponent } from '../payment-form-options/credit-card-form/credit-card-form.component';
import { PaypalFormComponent } from '../payment-form-options/paypal-form/paypal-form.component';
import { PixFormComponent } from '../payment-form-options/pix-form/pix-form.component';
import { AsyncPipe } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { IPaymentMethod } from '../../../../../core/models/payment-method';
import { PaymentMethodService } from '../../../../../services/payment-method.service';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-payment-selector',
  standalone: true,
  imports: [
    CreditCardFormComponent,
    PaypalFormComponent,
    PixFormComponent,
    AsyncPipe,
    ReactiveFormsModule,
    FormsModule,
  ],
  templateUrl: './payment-selector.component.html',
  styleUrl: './payment-selector.component.css',
})
export class PaymentSelectorComponent implements OnInit {
  paymentMethodService = inject(PaymentMethodService);
  paymentMethods!: IPaymentMethod[];
  paymentMethods$: Observable<IPaymentMethod[]> | undefined;
  paymentMethodForm!: FormGroup;
  fb = inject(FormBuilder);
  @Output() formValidityChanged: EventEmitter<boolean> =
    new EventEmitter<boolean>();
  activeFormValidity = false;
  store = inject(Store);
  @Output() paymentMethodOutput: EventEmitter<string> =
    new EventEmitter<string>();

  updateFormValidity(isValid: boolean) {
    this.activeFormValidity = isValid;
    this.formValidityChanged.emit(this.activeFormValidity);
  }

  constructor() {
    this.paymentMethods$ = this.paymentMethodService.paymentMethods$;
  }

  getPaymentMethods() {
    return this.paymentMethodService
      .getPaymentMethods()
      .subscribe((paymentMethods) => {
        this.paymentMethods = paymentMethods;
      });
  }

  ngOnInit(): void {
    this.getPaymentMethods();
    this.paymentMethodForm = this.fb.group({
      paymentMethod: ['', Validators.required],
    });

    console.log(localStorage.getItem('payment'));
    
    this.paymentMethodForm.valueChanges.subscribe((payment: string) => {
      this.paymentMethodOutput.emit(payment);
    });
  }
}
