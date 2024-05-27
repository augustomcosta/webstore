import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { select, Store } from '@ngrx/store';
import { selectStepperStep } from '../state/checkout/checkout.selectors';
import { ShippingFormComponent } from '../components/shipping-form/shipping-form.component';
import { PaymentFormComponent } from '../components/payment-form/payment-form.component';
import { OrderSummaryComponent } from '../components/order-summary/order-summary.component';
import { CartSummaryComponent } from '../components/cart-summary/cart-summary.component';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [
    RouterLink,
    FormsModule,
    ReactiveFormsModule,
    AsyncPipe,
    CurrencyPipe,
    ShippingFormComponent,
    PaymentFormComponent,
    OrderSummaryComponent,
    CartSummaryComponent,
  ],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css',
})
export class CheckoutComponent implements OnInit {
  http = inject(HttpClient);
  store = inject(Store);
  stepperStep$: Observable<number>;

  constructor() {
    this.stepperStep$ = this.store.pipe(select(selectStepperStep));
  }

  ngOnInit(): void {}
}
