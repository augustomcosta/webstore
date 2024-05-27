import { Component, inject, Input, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import * as CheckoutActions from '../../state/checkout/checkout.actions';
import { placeOrderSuccess } from '../../state/checkout/checkout.actions';
import { BasketService } from '../../../../basket/data/services/basket.service';
import { Observable } from 'rxjs';
import { Basket, BasketTotals } from '../../../../../core/models/basket';
import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { IPaymentMethod } from '../../../../../core/models/payment-method';
import { RouterLink } from '@angular/router';
import { selectShippingMethod } from '../../state/shipping/shipping.selectors';
import { IDeliveryMethod } from '../../../../../core/models/delivery-method';
import { OrderService } from '../../../../orders/data/services/order.service';
import { Order } from '../../../../../core/models/order';

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
  basket$: Observable<Basket> | undefined;
  basket!: Basket;
  basketTotal$: Observable<BasketTotals> | undefined;
  @Input() paymentMethod!: IPaymentMethod;
  paymentSelected!: string;
  shippingSelected!: IDeliveryMethod;
  orderService = inject(OrderService);
  createdOrder!: Order;
  isOrderSuccess: boolean = false;

  constructor() {}

  getBasket() {
    this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });
  }

  previousStep() {
    this.store.dispatch(CheckoutActions.previousStep());
  }

  getCurrentPaymentMethod() {
    const jsonString = localStorage.getItem('payment')!;
    const jsonObject = JSON.parse(jsonString);
    this.paymentSelected = jsonObject.value.paymentMethod;
  }

  getCurrentShippingMethod() {
    this.store.pipe(select(selectShippingMethod)).subscribe((shipping) => {
      if (shipping) {
        this.shippingSelected = shipping;
      }
    });
  }

  placeOrder() {
    this.orderService
      .placeOrder(this.shippingSelected.id)
      .subscribe((order) => {
        this.createdOrder = order;
        this.isOrderSuccess = true;
        this.store.dispatch(placeOrderSuccess());
        this.basketService.resetUserBasket();
      });
  }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.basketTotal$ = this.basketService.basketTotal$;
    this.getBasket();
    this.getCurrentPaymentMethod();
    this.getCurrentShippingMethod();
  }
}
