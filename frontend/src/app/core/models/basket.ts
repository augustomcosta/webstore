import { v4 as uuid } from 'uuid';
import { BasketItem } from './basket-item';

export interface IBasket {
  id: string;
  userId: string;
  deliveryMethodId: string;
  createdAt: Date;
  paymentIntentId: string;
  shippingPrice: number;
  basketItems: BasketItem[];
}

export class Basket implements IBasket {
  id: string;
  deliveryMethodId!: string;
  paymentIntentId!: string;
  shippingPrice!: number;
  basketItems: BasketItem[];
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
