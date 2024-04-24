import {
  ChangeDetectorRef,
  Component,
  inject,
  Input,
  OnInit,
} from '@angular/core';
import { IProduct } from '../../core/models/IProduct';
import { Router, RouterLink } from '@angular/router';
import { BasketSummaryComponent } from '../basket/basket-summary/basket-summary.component';
import { BasketTotalsComponent } from '../basket/basket-totals/basket-totals.component';
import { CurrencyPipe, NgOptimizedImage } from '@angular/common';
import { BasketService } from '../../services/basket.service';
import { WishlistService } from '../../services/wishlist.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { IWishlist } from '../../core/models/wishlist';
import { AuthService } from '../../services/auth.service';
import { IBasket } from '../../core/models/basket';

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
  @Input() product!: IProduct;
  wishlist!: IWishlist;
  basketService = inject(BasketService);
  wishlistService = inject(WishlistService);
  wishlist$!: Observable<IWishlist>;
  protected authService = inject(AuthService);
  protected router = inject(Router);
  cdr = inject(ChangeDetectorRef);
  isLoggedIn$: Observable<boolean> | undefined;
  isLoggedIn: Boolean | undefined;
  basket: IBasket | undefined;
  basket$: Observable<IBasket> | undefined;
  isOnWishlist: boolean | undefined;
  isOnWishlist$: Observable<boolean> | undefined;
  isOnWishlistSource = new BehaviorSubject<boolean>(false);

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

  addItemToWishlist(item: IProduct) {
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
