import { Component, inject, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import * as CheckoutActions from '../data/checkout.actions';
import { BasketService } from '../../../services/basket.service';
import { Observable } from 'rxjs';
import { IBasket } from '../../../core/models/basket';

@Component({
  selector: 'app-order-summary',
  standalone: true,
  imports: [],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.css',
})
export class OrderSummaryComponent implements OnInit {
  store = inject(Store);
  basketService = inject(BasketService);
  basket$: Observable<IBasket> | undefined;
  basket!: IBasket;

  previousStep() {
    this.store.dispatch(CheckoutActions.previousStep());
  }

  getBasket() {
    this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });
  }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.getBasket();
  }
}
