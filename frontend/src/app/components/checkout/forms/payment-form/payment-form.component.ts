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
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-payment-form',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink, AsyncPipe],
  templateUrl: './payment-form.component.html',
  styleUrl: './payment-form.component.css',
})
export class PaymentFormComponent implements OnInit {
  paymentMethods: IPaymentMethod[] | undefined;
  paymentMethodService = inject(PaymentMethodService);
  paymentMethods$: Observable<IPaymentMethod[]> | undefined;
  paymentForm!: FormGroup;
  fb = inject(FormBuilder);
  store = inject(Store);

  constructor() {
    this.paymentMethods$ = this.paymentMethodService.paymentMethods$;

    this.paymentForm = this.fb.group({
      paymentMethod: ['', Validators.required],
    });
  }

  toggleDropdown() {
    const dropUp = document.getElementById('drop-up-content');

    if (dropUp!.style.display === 'block') {
      dropUp!.style.display = 'none';
    }

    dropUp!.style.display = 'block';
  }

  getPaymentMethods() {
    return this.paymentMethodService
      .getPaymentMethods()
      .subscribe((paymentMethods) => {
        this.paymentMethods = paymentMethods;
      });
  }

  goToNextStep() {
    this.store.dispatch(CheckoutActions.nextStep());
  }

  ngOnInit(): void {
    this.getPaymentMethods();
  }
}
