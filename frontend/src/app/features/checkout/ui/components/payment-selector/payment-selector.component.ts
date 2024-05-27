import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { CreditCardFormComponent } from '../credit-card-form/credit-card-form.component';
import { PixFormComponent } from '../pix-form/pix-form.component';
import { AsyncPipe } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { IPaymentMethod } from '../../../../../core/models/payment-method';
import { PaymentMethodService } from '../../../data/services/payment-method.service';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-payment-selector',
  standalone: true,
  imports: [
    CreditCardFormComponent,
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

  constructor() {
    this.paymentMethods$ = this.paymentMethodService.paymentMethods$;
  }

  updateFormValidity(isValid: boolean) {
    this.activeFormValidity = isValid;
    this.formValidityChanged.emit(this.activeFormValidity);
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
