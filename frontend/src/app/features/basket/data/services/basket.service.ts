import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Basket, BasketTotals } from '../../domain/models/basket';
import { BehaviorSubject, tap } from 'rxjs';
import { Product } from '../../../home/domain/models/product';
import { BasketItem } from '../../domain/models/basket-item';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;
  shipping: number = 0;
  basketSource = new BehaviorSubject<Basket>(null as any);
  basket$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<BasketTotals>(null as any);
  basketTotal$ = this.basketTotalSource.asObservable();

  constructor() {}

  getBasketFromLoggedUser() {
    const userId = localStorage.getItem('userId');
    return this.http
      .get<Basket>(this.apiUrl + `/Basket/get-by-user?userId=${userId}`)
      .pipe(
        tap((basket: Basket) => {
          this.basketSource.next(basket);
          this.shipping = basket.shippingPrice;
          this.calculateTotals();
        }),
      );
  }

  resetUserBasket() {
    return this.http
      .get<Basket>(
        this.apiUrl +
          `/Basket/reset-user-basket?userId=${localStorage.getItem('userId')}`,
      )
      .subscribe((basket: Basket) => {
        this.basketSource.next(basket);
      });
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  addItemToBasket(item: Product, quantity = 1) {
    let basket = this.getCurrentBasketValue();
    console.log(basket);
    const itemToAdd = this.mapProductToBasketItem(item);

    basket.basketItems = this.addOrUpdateItem(
      basket.basketItems,
      itemToAdd,
      quantity,
    );

    this.setBasket(basket);
  }

  setBasket(basket: Basket) {
    return this.http
      .put<Basket>(this.apiUrl + '/Basket/update-basket', basket)
      .subscribe((basket: Basket) => {
        this.basketSource.next(basket);
        this.calculateTotals();
      });
  }

  removeItemFromBasket(itemId: string, quantity = 1) {
    const basket = this.getCurrentBasketValue();
    if (!basket) return;

    const itemIndex = basket.basketItems.findIndex((b) => b.id === itemId);
    if (itemIndex !== -1) {
      basket.basketItems[itemIndex].quantity -= quantity;
      if (basket.basketItems[itemIndex].quantity <= 0) {
        basket.basketItems.splice(itemIndex, 1);
      }
      this.setBasket(basket);
      window.location.reload();
    }
  }

  calculateTotals() {
    const basket = this.getCurrentBasketValue();
    const shipping = this.shipping;
    const subTotal = basket.basketItems.reduce(
      (a, b) => b.price * b.quantity + a,
      0,
    );
    const total = subTotal + shipping;
    this.basketTotalSource.next({ shipping, total, subTotal });
    return subTotal;
  }

  private addOrUpdateItem(
    items: BasketItem[],
    itemToAdd: BasketItem,
    quantity: number,
  ): BasketItem[] {
    items = items || [];

    const item = items.find((x) => x.id === itemToAdd.id);
    if (item) item.quantity += quantity;
    else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;
  }

  private mapProductToBasketItem(item: Product): BasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      productImgUrl: item.imageUrl,
      quantity: 0,
      brand: item.brandName,
      category: item.categoryName,
    };
  }
}
