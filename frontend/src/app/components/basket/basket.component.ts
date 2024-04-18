import { Component, inject, OnInit } from '@angular/core';
import { BasketSummaryComponent } from './basket-summary/basket-summary.component';
import { BasketTotalsComponent } from './basket-totals/basket-totals.component';
import { IBasket, IBasketTotals } from '../../core/models/basket';
import { Observable } from 'rxjs';
import { BasketService } from '../../services/basket.service';

import { RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [
    BasketSummaryComponent,
    BasketTotalsComponent,
    RouterLink,
    AsyncPipe,
  ],
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.css',
})
export class BasketComponent implements OnInit {
  basket$: Observable<IBasket> | undefined;
  basketTotals$: Observable<IBasketTotals> | undefined;
  basketService = inject(BasketService);
  basket!: IBasket;
  isLoggedIn$: Observable<boolean> | undefined;
  authService = inject(AuthService);

  public getBasketFromLoggedUser() {
    return this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });
  }

  public deleteBasket(basket: IBasket) {
    this.basketService.deleteBasket(basket);
  }

  ngOnInit(): void {
    this.isLoggedIn$ = this.authService.isLoggedIn();
    this.basket$ = this.basketService.basket$;
    this.basketTotals$ = this.basketService.basketTotal$;
    this.getBasketFromLoggedUser();
  }
}
