import { Component, inject, Input, OnInit } from '@angular/core';
import { Product } from '../../../../home/domain/models/product';
import { Router, RouterLink } from '@angular/router';
import { BasketSummaryComponent } from '../../../../basket/ui/components/basket-summary/basket-summary.component';
import { BasketTotalsComponent } from '../../../../basket/ui/components/basket-totals/basket-totals.component';
import { CurrencyPipe, NgOptimizedImage } from '@angular/common';
import { BasketService } from '../../../../basket/data/services/basket.service';
import { WishlistService } from '../../../../wishlist/data/services/wishlist.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { IWishlist } from '../../../../wishlist/domain/models/wishlist';
import { AuthService } from '../../../../auth/data/services/auth.service';
import { Basket } from '../../../../basket/domain/models/basket';

@Component({
  selector: 'app-item-details',
  standalone: true,
  imports: [
    BasketSummaryComponent,
    BasketTotalsComponent,
    RouterLink,
    NgOptimizedImage,
    CurrencyPipe,
  ],
  templateUrl: './item-details.component.html',
  styleUrl: './item-details.component.css',
})
export class ItemDetailsComponent implements OnInit {
  @Input() product!: Product;
  wishlist!: IWishlist;
  basketService = inject(BasketService);
  wishlistService = inject(WishlistService);
  wishlist$!: Observable<IWishlist>;
  isLoggedIn$: Observable<boolean> | undefined;
  isLoggedIn: Boolean | undefined;
  basket: Basket | undefined;
  basket$: Observable<Basket> | undefined;
  isOnWishlist: boolean | undefined;
  isOnWishlist$: Observable<boolean> | undefined;
  isOnWishlistSource = new BehaviorSubject<boolean>(false);
  protected authService = inject(AuthService);
  protected router = inject(Router);

  constructor() {
    this.isLoggedIn$?.subscribe((isLogged) => {
      this.isLoggedIn = isLogged;
    });

    this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });

    this.wishlistService.getWishlistFromLoggedUser()!.subscribe((wishlist) => {
      this.wishlist = wishlist;
    });
  }

  addItemToWishlist(item: Product) {
    this.wishlistService.addItemToWishlist(item);
  }

  addItemToBasket() {
    if (!this.isLoggedIn$) {
      this.router.navigate(['/login']).then();
      return;
    }
    this.basketService.addItemToBasket(this.product, 1);
  }

  isItemOnWishlist() {
    this.wishlistService.getWishlistFromLoggedUser()!.subscribe((wishlist) => {
      this.isOnWishlist =
        wishlist.wishlistItems.some((w) => w.id == this.product.id) ?? false;
      this.isOnWishlistSource.next(this.isOnWishlist);
    });
    return this.isOnWishlistSource.asObservable();
  }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.wishlist$ = this.wishlistService.wishlist$;
    this.isLoggedIn$ = this.authService.isLoggedIn();
    this.isOnWishlist$ = this.isItemOnWishlist();
  }
}
