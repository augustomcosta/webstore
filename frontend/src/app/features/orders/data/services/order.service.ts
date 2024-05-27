import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../../environments/environment';
import { Basket } from '../../../basket/domain/models/basket';
import { BasketService } from '../../../basket/data/services/basket.service';
import { Order } from '../../domain/models/order';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;
  basketService = inject(BasketService);
  basket: Basket | undefined;
  userOrdersSource = new BehaviorSubject<Order[]>([]);
  userOrders$ = this.userOrdersSource.asObservable();

  constructor() {
    this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });
  }

  placeOrder(deliveryMethodId: string): Observable<Order> {
    return this.http.post<Order>(
      this.apiUrl +
        `/Order/create-order?basketId=${this.basket?.id}&userId=${localStorage.getItem('userId')}&deliveryMethodId=${deliveryMethodId}`,
      {},
    );
  }

  getAllOrdersForUser() {
    return this.http
      .get<
        Order[]
      >(this.apiUrl + `/Order/get-all-orders-for-user?userId=${localStorage.getItem('userId')}`)
  }

  getOrderById(orderId: string): Observable<Order> {
    return this.http.get<Order>(this.apiUrl + `/Order/${orderId}`);
  }
}
