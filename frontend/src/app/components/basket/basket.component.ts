import { Component, inject, OnInit } from '@angular/core';
import { BasketSummaryComponent } from '../basket-summary/basket-summary.component';
import { BasketTotalsComponent } from '../basket-totals/basket-totals.component';
import { IBasket, IBasketTotals } from '../../core/models/basket';
import { async, Observable } from 'rxjs';
import { BasketService } from '../../services/basket.service';
import { IBasketItem } from '../../core/models/basketItem';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [BasketSummaryComponent, BasketTotalsComponent, RouterLink],
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.css',
})
export class BasketComponent implements OnInit {
  basket$: Observable<IBasket> | undefined;
  basketTotals$: Observable<IBasketTotals> | undefined;
  basketService = inject(BasketService);
  // @ts-ignore
  basketId: string = localStorage.getItem('basket_id');
  // @ts-ignore
  basket: IBasket;

  public getBasketFromLoggedUser() {
    return this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });
  }

  public deleteBasket(basket: IBasket) {
    this.basketService.deleteBasket(basket);
  }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.basketTotals$ = this.basketService.basketTotal$;
    this.getBasketFromLoggedUser();
  }
}
