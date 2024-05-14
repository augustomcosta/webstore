import { Component, inject, Input, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import * as CheckoutActions from '../data/checkout.actions';
import { BasketService } from '../../../services/basket.service';
import { Observable } from 'rxjs';
import { IBasket, IBasketTotals } from '../../../core/models/basket';
import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { IPaymentMethod } from '../../../core/models/payment-method';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-order-summary',
  standalone: true,
  imports: [CurrencyPipe, AsyncPipe, RouterLink],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.css',
})
export class OrderSummaryComponent implements OnInit {
  store = inject(Store);
  basketService = inject(BasketService);
  basket$: Observable<IBasket> | undefined;
  basket!: IBasket;
  basketTotal$: Observable<IBasketTotals> | undefined;
  @Input() paymentMethod!: IPaymentMethod;

  constructor() {}

  getBasket() {
    this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });
  }

  previousStep() {
    this.store.dispatch(CheckoutActions.previousStep());
  }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.basketTotal$ = this.basketService.basketTotal$;
    this.getBasket();
  }
}
