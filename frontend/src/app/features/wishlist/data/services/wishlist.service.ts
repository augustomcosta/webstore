import { inject, Injectable } from '@angular/core';
import { IWishlist } from '../../domain/models/wishlist';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../../environments/environment';
import { Product } from '../../../home/domain/models/product';
import { BehaviorSubject, tap } from 'rxjs';
import { WishlistItem } from '../../domain/models/wishlist-item';

@Injectable({
  providedIn: 'root',
})
export class WishlistService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;
  wishlistSource = new BehaviorSubject<IWishlist>(null as any);
  wishlist$ = this.wishlistSource.asObservable();

  constructor() {}

  getWishlistFromLoggedUser() {
    const userId = localStorage.getItem('userId');
    console.log(userId);
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

  addItemToWishlist(item: Product) {
    let wishlist = this.getCurrentWishlistValue();
    const itemToAdd = this.mapProductToWishlistItem(item);

    this.getWishlistFromLoggedUser()?.subscribe((wishlist$) => {
      wishlist = wishlist$;
      wishlist.wishlistItems = this.addItem(wishlist.wishlistItems, itemToAdd);
      this.setWishlist(wishlist);
    });
  }

  removeItemFromWishlist(itemId: string) {
    const wishlist = this.getCurrentWishlistValue();

    const itemIndex = wishlist.wishlistItems.findIndex((b) => b.id === itemId);

    wishlist.wishlistItems.splice(itemIndex, 1);

    this.setWishlist(wishlist);
  }

  getCurrentWishlistValue() {
    return this.wishlistSource.value;
  }

  setWishlist(wishlist: IWishlist) {
    return this.http
      .put<IWishlist>(this.apiUrl + `/Wishlist/update-wishlist`, wishlist)
      .subscribe((updatedWishlist) =>
        this.wishlistSource.next(updatedWishlist),
      );
  }

  mapWishlistItemToProduct(item: WishlistItem): Product {
    return {
      id: item.id,
      name: item.productName,
      description: '',
      price: item.price,
      imageUrl: item.productImgUrl,
      brandName: item.brand,
      categoryName: item.category,
    };
  }

  private addItem(
    items: WishlistItem[],
    itemToAdd: WishlistItem,
  ): WishlistItem[] {
    const updatedItems = items;
    const existingItemIndex = updatedItems.findIndex(
      (x) => x.id === itemToAdd.id,
    );

    if (existingItemIndex !== -1) {
      console.log('Item is already on the wishlist');
      return items;
    }

    updatedItems.push(itemToAdd);

    return updatedItems;
  }

  private mapProductToWishlistItem(item: Product): WishlistItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      productImgUrl: item.imageUrl,
      brand: item.brandName,
      category: item.categoryName,
    };
  }
}
