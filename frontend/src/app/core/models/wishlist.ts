import { IProduct } from './IProduct';
import { v4 as uuid } from 'uuid';

export interface IWishlist {
  id: string;
  userId: string;
  wishlistItems: IProduct[];
}

export class Wishlist implements IWishlist {
  id: string;
  userId: string;
  wishlistItems: IProduct[];

  constructor() {
    this.id = uuid();
    this.userId = localStorage.getItem('userId') ?? '';
    this.wishlistItems = [];
  }
}
