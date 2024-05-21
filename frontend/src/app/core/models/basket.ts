import { BasketItem } from './basket-item';

export interface Basket {
  id: string;
  userId: string;
  deliveryMethodId: string;
  createdAt: Date;
  shippingPrice: number;
  basketItems: BasketItem[];
}

export interface BasketTotals {
  shipping: number;
  subTotal: number;
  total: number;
}
