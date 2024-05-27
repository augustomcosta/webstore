import { Component, inject, OnInit } from '@angular/core';
import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { BasketService } from '../../../../basket/data/services/basket.service';
import { Basket, BasketTotals } from '../../../../../core/models/basket';
import { Observable } from 'rxjs';
import { DeliveryMethodService } from '../../../../../services/delivery-method.service';
import { IDeliveryMethod } from '../../../../../core/models/delivery-method';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Store } from '@ngrx/store';
import * as CheckoutActions from '../../state/shipping/shipping.actions';

@Component({
  selector: 'app-cart-summary',
  standalone: true,
  imports: [AsyncPipe, CurrencyPipe, ReactiveFormsModule],
  templateUrl: './cart-summary.component.html',
  styleUrl: './cart-summary.component.css',
})
export class CartSummaryComponent implements OnInit {
  basketService = inject(BasketService);
  basket: Basket | undefined;
  basket$: Observable<Basket> | undefined;
  basketTotal$: Observable<BasketTotals> | undefined;
  deliveryMethodService = inject(DeliveryMethodService);
  deliveryMethods: IDeliveryMethod[] = [];
  deliveryMethods$!: Observable<IDeliveryMethod[]>;
  deliveryMethodForm!: FormGroup;
  deliveryPrice!: number;
  deliveryMethodSelected!: IDeliveryMethod;
  fb = inject(FormBuilder);
  store = inject(Store);

  constructor() {
    this.deliveryMethodForm = this.fb.group({
      deliveryMethod: ['', Validators.required],
    });

    this.getDeliveryMethods().subscribe((deliveryMethods) => {
      this.deliveryMethods = deliveryMethods;
      console.log(this.deliveryMethods);
    });
  }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;

    this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });

    this.basketTotal$ = this.basketService.basketTotal$;

    this.deliveryMethods$ = this.deliveryMethodService.deliveryMethods$;
  }

  getShippingPrice() {
    this.deliveryMethodSelected =
      this.deliveryMethodForm.get('deliveryMethod')?.value;

    this.deliveryPrice = this.deliveryMethodSelected.price;

    this.basketTotal$?.subscribe((basket) => {
      basket.shipping = this.deliveryPrice;
    });

    this.store.dispatch(
      CheckoutActions.saveShippingMethod({
        shippingMethod: this.deliveryMethodForm.get('deliveryMethod')?.value,
      }),
    );
  }

  setBasketShippingPrice() {
    this.basket!.shippingPrice = this.deliveryPrice;
    this.basket!.deliveryMethodId = this.deliveryMethodSelected.id;
    this.basketService.setBasket(this.basket!);
  }

  getDeliveryMethods() {
    return this.deliveryMethodService.getDeliveryMethods();
  }
}
