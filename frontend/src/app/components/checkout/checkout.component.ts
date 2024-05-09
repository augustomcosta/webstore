import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { BasketService } from '../../services/basket.service';
import { IBasket, IBasketTotals } from '../../core/models/basket';
import { Observable } from 'rxjs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { select, Store } from '@ngrx/store';
import { selectStepperStep } from './data/checkout.selectors';
import { ShippingFormComponent } from './forms/shipping-form/shipping-form.component';
import { PaymentFormComponent } from './forms/payment-form/payment-form.component';

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
  ],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css',
})
export class CheckoutComponent implements OnInit {
  basketService = inject(BasketService);
  basket: IBasket | undefined;
  basket$: Observable<IBasket> | undefined;
  basketTotal$: Observable<IBasketTotals> | undefined;
  http = inject(HttpClient);
  store = inject(Store);
  stepperStep$: Observable<number>;

  constructor() {
    this.basketTotal$ = this.basketService.basketTotal$;
    this.stepperStep$ = this.store.pipe(select(selectStepperStep));
    this.basket$ = this.basketService.basket$;
    this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });
    this.basketTotal$ = this.basketService.basketTotal$;
  }

  ngOnInit(): void {}
}
