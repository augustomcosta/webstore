import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { IBasket } from '../core/models/basket';
import { BasketService } from './basket.service';
import { Order } from '../core/models/order';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;
  basketService = inject(BasketService);
  basket: IBasket | undefined;

  placeOrder(deliveryMethodId: string): Observable<Order> {
    return this.http.post<Order>(
      this.apiUrl +
        `/Order/create-order?basketId=${this.basket?.id}&userId=${localStorage.getItem('userId')}&deliveryMethodId=${deliveryMethodId}`,
      {},
    );
  }

  constructor() {
    this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });
  }
}
