import {inject, Injectable} from '@angular/core';
import {IWishlist, Wishlist} from "../core/models/wishlist";
import {HttpClient} from "@angular/common/http";
import {environment} from "../environments/environment";


@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;

  constructor() { }

  setWishlist(){

  }

  private createWishlist(): IWishlist {
    const wishlist = new Wishlist();
    localStorage.setItem('wishlist_id', wishlist.id);

    return wishlist;
  }
}


