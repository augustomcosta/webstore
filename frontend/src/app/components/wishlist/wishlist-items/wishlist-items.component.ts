import { Component, inject, Input, OnInit } from '@angular/core';
import { CurrencyPipe, NgOptimizedImage } from '@angular/common';
import { RouterLink } from '@angular/router';
import { IWishlist } from '../../../core/models/wishlist';
import { Observable } from 'rxjs';
import { WishlistService } from '../../../services/wishlist.service';
import { IProduct } from '../../../core/models/IProduct';
import { BasketService } from '../../../services/basket.service';
import { IWishlistItem } from '../../../core/models/wishlistItem';

@Component({
  selector: 'app-wishlist-items',
  standalone: true,
  imports: [NgOptimizedImage, RouterLink, CurrencyPipe],
  templateUrl: './wishlist-items.component.html',
  styleUrl: './wishlist-items.component.css',
})
export class WishlistItemsComponent implements OnInit {
  wishlistService = inject(WishlistService);
  @Input() wishlist!: IWishlist;
  wishlist$!: Observable<IWishlist>;
  basketService = inject(BasketService);

  removeItemFromWishlist(itemId: string) {
    return this.wishlistService.removeItemFromWishlist(itemId);
  }

  addItemToBasket(product: IWishlistItem) {
    const item = this.wishlistService.mapWishlistItemToProduct(product);
    this.basketService.addItemToBasket(item);
  }

  ngOnInit(): void {
    this.wishlist$ = this.wishlistService.wishlist$;
  }
}