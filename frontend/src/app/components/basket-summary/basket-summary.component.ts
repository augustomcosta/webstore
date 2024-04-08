import { Component, inject, Input, OnInit } from '@angular/core';
import { CurrencyPipe, NgOptimizedImage } from '@angular/common';
import { BasketService } from '../../services/basket.service';
import { IBasket } from '../../core/models/basket';
import { Observable } from 'rxjs';
import { IBasketItem } from '../../core/models/basketItem';

@Component({
  selector: 'app-basket-summary',
  standalone: true,
  imports: [NgOptimizedImage, CurrencyPipe],
  templateUrl: './basket-summary.component.html',
  styleUrl: './basket-summary.component.css',
})
export class BasketSummaryComponent {
  basketService = inject(BasketService);
  @Input() basket!: IBasket;

  removeItemFromBasket(item: IBasketItem) {
    return this.basketService.removeItemFromBasket(item);
  }
}
