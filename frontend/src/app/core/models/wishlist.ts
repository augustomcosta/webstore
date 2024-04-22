import { IProduct } from './IProduct';
import { v4 as uuid } from 'uuid';
import { IWishlistItem } from './wishlistItem';

export interface IWishlist {
  id: string;
  userId: string;
  wishlistItems: IWishlistItem[];
}

export class Wishlist implements IWishlist {
  id: string;
  userId: string;
  wishlistItems: IWishlistItem[];

  constructor() {
    this.id = uuid();
    this.userId = localStorage.getItem('userId') ?? '';
    this.wishlistItems = [];
  }
}
