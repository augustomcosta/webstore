import { Component, inject, OnInit } from '@angular/core';
import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { BasketService } from '../../../services/basket.service';
import { IBasket, IBasketTotals } from '../../../core/models/basket';
import { Observable } from 'rxjs';
import { DeliveryMethodService } from '../../../services/delivery-method.service';
import { IDeliveryMethod } from '../../../core/models/delivery-method';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-cart-summary',
  standalone: true,
  imports: [AsyncPipe, CurrencyPipe, ReactiveFormsModule],
  templateUrl: './cart-summary.component.html',
  styleUrl: './cart-summary.component.css',
})
export class CartSummaryComponent implements OnInit {
  basketService = inject(BasketService);
  basket: IBasket | undefined;
  basket$: Observable<IBasket> | undefined;
  basketTotal$: Observable<IBasketTotals> | undefined;
  deliveryMethodService = inject(DeliveryMethodService);
  deliveryMethods: IDeliveryMethod[] = [];
  deliveryMethods$!: Observable<IDeliveryMethod[]>;
  deliveryMethodForm!: FormGroup;
  deliveryPrice!: number;
  fb = inject(FormBuilder);

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;

    this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });

    this.basketTotal$ = this.basketService.basketTotal$;

    this.deliveryMethods$ = this.deliveryMethodService.deliveryMethods$;

    this.getDeliveryMethods();
  }

  constructor() {
    this.deliveryMethodForm = this.fb.group({
      deliveryMethod: ['', Validators.required],
    });
  }

  getShippingPrice() {
    this.deliveryPrice = this.deliveryMethodForm.get('deliveryMethod')?.value;
    this.basketTotal$?.subscribe((basket) => {
      basket.shipping = this.deliveryPrice;
    });
  }

  getDeliveryMethods() {
    return this.deliveryMethodService
      .getDeliveryMethods()
      .subscribe((deliveryMethods) => {
        this.deliveryMethods = deliveryMethods;
      });
  }
}
