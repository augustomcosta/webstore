import { Component, inject, OnInit } from '@angular/core';
import { BasketSummaryComponent } from '../components/basket-summary/basket-summary.component';
import { BasketTotalsComponent } from '../components/basket-totals/basket-totals.component';
import { Basket, BasketTotals } from '../../domain/models/basket';
import { Observable } from 'rxjs';
import { BasketService } from '../../data/services/basket.service';

import { RouterLink } from '@angular/router';
import { AuthService } from '../../../auth/data/services/auth.service';
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
  basket$: Observable<Basket> | undefined;
  basketTotals$: Observable<BasketTotals> | undefined;
  basketService = inject(BasketService);
  basket!: Basket;
  isLoggedIn$: Observable<boolean> | undefined;
  authService = inject(AuthService);

  public getBasketFromLoggedUser() {
    return this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });
  }

  ngOnInit(): void {
    this.isLoggedIn$ = this.authService.isLoggedIn();
    this.basket$ = this.basketService.basket$;
    this.basketTotals$ = this.basketService.basketTotal$;
    this.getBasketFromLoggedUser();
  }
}
