import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { Router, RouterLink } from '@angular/router';
import { IProduct } from '../../../core/models/IProduct';
import { CurrencyPipe, NgOptimizedImage } from '@angular/common';
import { BasketService } from '../../../services/basket.service';
import { Input } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { IBasket, IBasketTotals } from '../../../core/models/basket';
import { AuthService } from '../../../services/auth.service';

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
  basket$: Observable<IBasket> | undefined;
  basketTotals$: Observable<IBasketTotals> | undefined;
  isLoggedIn$: Observable<boolean> | undefined;
  isLoggedIn: boolean | undefined;
  protected authService = inject(AuthService);
  protected router = inject(Router);

  @Input()
  product!: IProduct;
  protected readonly Math = Math;

  constructor() {
    this.isLoggedIn$?.subscribe((isLogged) => {
      this.isLoggedIn = isLogged;
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
