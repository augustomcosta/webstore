import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Basket, IBasket, IBasketTotals } from '../core/models/basket';
import { BehaviorSubject, tap } from 'rxjs';
import { IProduct } from '../core/models/IProduct';
import { IBasketItem } from '../core/models/basketItem';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;
  shipping: number = 0;
  private basketSource = new BehaviorSubject<IBasket>(null as any);
  basket$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null as any);
  basketTotal$ = this.basketTotalSource.asObservable();

  constructor() {}

  getBasketFromLoggedUser() {
    const userId = localStorage.getItem('userId');
    return this.http
      .get<IBasket>(this.apiUrl + `/Basket/get-by-user?userId=${userId}`)
      .pipe(
        tap((basket: IBasket) => {
          this.basketSource.next(basket);
          this.shipping = basket.shippingPrice;
          this.calculateTotals();
        }),
      );
  }

  getBasket(id: string) {
    return this.http.get<IBasket>(this.apiUrl + `/Basket?basketId=${id}`).pipe(
      tap((basket: IBasket) => {
        this.basketSource.next(basket);
        this.shipping = basket.shippingPrice;
        this.calculateTotals();
      }),
    );
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct, quantity = 1) {
    let basket = this.getCurrentBasketValue();
    if (!basket) {
      basket = this.createBasket();
    }

    const itemToAdd = this.mapProductToBasketItem(item);

    basket.basketItems = this.addOrUpdateItem(
      basket.basketItems,
      itemToAdd,
      quantity,
    );

    this.setBasket(basket);
  }

  setBasket(basket: IBasket) {
    return this.http
      .post<IBasket>(this.apiUrl + '/Basket/update-basket', basket)
      .subscribe((basket: IBasket) => {
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
      if (basket.basketItems.length > 0) this.setBasket(basket);
      else {
        this.deleteBasket(basket);
      }
    }
  }

  deleteBasket(basket: IBasket) {
    return this.http
      .delete(this.apiUrl + `/Basket?basketId=${basket.id}`)
      .subscribe(() => {
        this.basketSource.next(null as any);
        this.basketTotalSource.next(null as any);
        localStorage.removeItem('basket_id');
        window.location.reload();
      });
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

  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);

    return basket;
  }

  private addOrUpdateItem(
    items: IBasketItem[],
    itemToAdd: IBasketItem,
    quantity: number,
  ): IBasketItem[] {
    items = items || [];

    const item = items.find((x) => x.id === itemToAdd.id);
    if (item) item.quantity += quantity;
    else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;
  }

  private mapProductToBasketItem(item: IProduct): IBasketItem {
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
