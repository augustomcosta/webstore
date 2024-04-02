import { IBasketItem } from './BasketItem';
import { v4 as uuid } from 'uuid';

export interface IBasket {
  id: string;
  deliveryMethodId: string;
  paymentIntentId: string;
  shippingPrice: number;
  basketItems: IBasketItem[];
}

// @ts-ignore
export class Basket implements IBasket {
  id = uuid();
  items: IBasketItem[] = [];
}

export interface IBasketTotals {
  shipping: number;
  subTotal: number;
  total: number;
}
