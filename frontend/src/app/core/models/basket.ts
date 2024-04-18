import { v4 as uuid } from 'uuid';
import { IBasketItem } from './basketItem';

export interface IBasket {
  id: string;
  userId: string;
  deliveryMethodId: string;
  createdAt: Date;
  paymentIntentId: string;
  shippingPrice: number;
  basketItems: IBasketItem[];
}

export class Basket implements IBasket {
  id: string;
  deliveryMethodId!: string;
  paymentIntentId!: string;
  shippingPrice!: number;
  basketItems: IBasketItem[];
  userId: string;
  createdAt: Date;

  constructor() {
    this.id = uuid();
    this.basketItems = [];
    this.userId = localStorage.getItem('userId') ?? '';
    this.createdAt = new Date();
  }
}

export interface IBasketTotals {
  shipping: number;
  subTotal: number;
  total: number;
}
