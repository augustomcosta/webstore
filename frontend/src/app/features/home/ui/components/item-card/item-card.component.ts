import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { Router, RouterLink } from '@angular/router';
import { Product } from '../../../../../core/models/product';
import { CurrencyPipe, NgOptimizedImage } from '@angular/common';
import { BasketService } from '../../../../basket/data/services/basket.service';
import { Input } from '@angular/core';
import { Observable } from 'rxjs';
import { Basket, BasketTotals } from '../../../../../core/models/basket';
import { AuthService } from '../../../../auth/data/services/auth.service';

@Component({
  selector: 'item-card-app',
  templateUrl: 'item-card.component.html',
  styleUrl: 'item-card.component.css',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule,
    RouterLink,
    NgOptimizedImage,
    CurrencyPipe,
  ],
})
export class ItemCardComponent implements OnInit {
  basketService = inject(BasketService);
  basket$: Observable<Basket> | undefined;
  basketTotals$: Observable<BasketTotals> | undefined;
  isLoggedIn$: Observable<boolean> | undefined;
  isLoggedIn: boolean | undefined;
  basket: Basket | undefined;
  @Input()
  product!: Product;
  protected authService = inject(AuthService);
  protected router = inject(Router);
  protected readonly Math = Math;

  constructor() {
    this.isLoggedIn$?.subscribe((isLogged) => {
      this.isLoggedIn = isLogged;
    });

    this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });
  }

  addItemToBasket() {
    if (!this.isLoggedIn$) {
      this.router.navigate(['/login']).then();
      return;
    }
    this.basketService.addItemToBasket(this.product, 1);
  }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.basketTotals$ = this.basketService.basketTotal$;
    this.isLoggedIn$ = this.authService.isLoggedIn();
  }
}
