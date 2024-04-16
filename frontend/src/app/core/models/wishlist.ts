import {IProduct} from "./IProduct";
import { v4 as uuid } from 'uuid';

export interface IWishlist {
  id: string;
  wishlistItems: IProduct[];
}

export class Wishlist implements IWishlist {
  id: string;
  wishlistItems: IProduct[];

  constructor() {
    this.id = uuid();
    this.wishlistItems = [];
  }
}
