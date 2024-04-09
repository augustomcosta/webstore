import { v4 as uuid } from 'uuid';
// @ts-ignore
import { IBasketItem } from './basketItem';

export interface IBasket {
  id: string;
  userId: string;
  deliveryMethodId: string;
  paymentIntentId: string;
  shippingPrice: number;
  basketItems: IBasketItem[];
}

// @ts-ignore
export class Basket implements IBasket {
  id: string;
  // @ts-ignore
  deliveryMethodId: string;
  // @ts-ignore
  paymentIntentId: string;
  // @ts-ignore
  shippingPrice: number;
  basketItems: IBasketItem[];
  userId: string;

  constructor() {
    this.id = uuid();
    this.basketItems = [];
    // @ts-ignore
    this.userId = localStorage.getItem('userId') ?? '';
  }
}

export interface IBasketTotals {
  shipping: number;
  subTotal: number;
  total: number;
}