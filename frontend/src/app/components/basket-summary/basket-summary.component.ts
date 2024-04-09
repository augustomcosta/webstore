import { Component, inject, Input, OnInit } from '@angular/core';
import { AsyncPipe, CurrencyPipe, NgOptimizedImage } from '@angular/common';
import { BasketService } from '../../services/basket.service';
import { IBasket } from '../../core/models/basket';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-basket-summary',
  standalone: true,
  imports: [NgOptimizedImage, CurrencyPipe, AsyncPipe],
  templateUrl: './basket-summary.component.html',
  styleUrl: './basket-summary.component.css',
})
export class BasketSummaryComponent implements OnInit {
  basketService = inject(BasketService);
  @Input() basket!: IBasket;
  // @ts-ignore
  basket$: Observable<IBasket>;

  removeItemFromBasket(itemId: string) {
    return this.basketService.removeItemFromBasket(itemId);
  }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }
}
