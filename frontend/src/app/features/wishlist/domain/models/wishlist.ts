import { v4 as uuid } from 'uuid';
import { WishlistItem } from './wishlist-item';

export interface IWishlist {
  id: string;
  userId: string;
  wishlistItems: WishlistItem[];
}

export class Wishlist implements IWishlist {
  id: string;
  userId: string;
  wishlistItems: WishlistItem[];

  constructor() {
    this.id = uuid();
    this.userId = localStorage.getItem('userId') ?? '';
    this.wishlistItems = [];
  }
}
