import { inject, Injectable } from '@angular/core';
import { IWishlist, Wishlist } from '../core/models/wishlist';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { IProduct } from '../core/models/IProduct';
import { BehaviorSubject, tap } from 'rxjs';
import { IBasket } from '../core/models/basket';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class WishlistService {
  http = inject(HttpClient);
  authService = inject(AuthService);
  apiUrl = environment.apiUrl;
  wishlistSource = new BehaviorSubject<IWishlist>(null as any);
  wishlist$ = this.wishlistSource.asObservable();

  constructor() {}

  getWishlistFromLoggedUser() {
    const userId = localStorage.getItem('userId');
    const wishlist = this.http
      .get<IWishlist>(this.apiUrl + `/Wishlist/get-by-userid?userId=${userId}`)
      .pipe(
        tap((wishlist: IWishlist) => {
          this.wishlistSource.next(wishlist);
        }),
      );
    if (!wishlist) {
      return null;
    }
    return wishlist;
  }

  addItemToWishlist(item: IProduct) {
    let wishlist = this.getCurrentWishlistValue();

    if (!wishlist) {
      wishlist = this.createWishlist();
    }

    if (this.isProductOnWishlist(item, wishlist)) {
      console.log('Product is already on the wishlist');
      return;
    }

    wishlist.wishlistItems.push(item);

    this.setWishlist(wishlist);
  }

  getCurrentWishlistValue() {
    return this.wishlistSource.value;
  }

  isProductOnWishlist(itemToAdd: IProduct, wishlist: IWishlist): boolean {
    return wishlist.wishlistItems.some((i) => i.name === itemToAdd.name);
  }

  setWishlist(wishlist: IWishlist) {
    const loggedUser = this.authService;
    return this.http
      .put<IWishlist>(this.apiUrl + `/Wishlist/update-wishlist`, wishlist)
      .subscribe((updatedWishlist) =>
        this.wishlistSource.next(updatedWishlist),
      );
  }

  private createWishlist(): IWishlist {
    const existingWishlist = this.getCurrentWishlistValue();
    if (existingWishlist) {
      return existingWishlist;
    }

    const wishlist = new Wishlist();

    localStorage.setItem('wishlist_id', wishlist.id);

    return wishlist;
  }
}
