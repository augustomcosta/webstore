import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { RouterLink } from '@angular/router';
import { IProduct } from '../../core/models/IProduct';
import { CurrencyPipe, NgOptimizedImage } from '@angular/common';
import { BasketService } from '../../services/basket.service';
import { Input } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket, IBasketTotals } from '../../core/models/basket';

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
  // @ts-ignore
  @Input() product: IProduct;
  protected readonly Math = Math;

  addItemToBasket() {
    this.basketService.addItemToBasket(this.product, 1);
  }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.basketTotals$ = this.basketService.basketTotal$;
  }
}
