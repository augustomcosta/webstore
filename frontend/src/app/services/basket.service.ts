import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Basket, IBasket } from '../core/models/Basket';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;

  constructor() {}

  getBasket(id: string) {
    return this.http.get(this.apiUrl + `Basket?basketId=${id}`);
  }

  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    // @ts-ignore
    return basket;
  }
}
