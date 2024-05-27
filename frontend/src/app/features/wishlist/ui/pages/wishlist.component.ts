import { Component, inject } from '@angular/core';
import { WishlistItemsComponent } from '../components/wishlist-items/wishlist-items.component';
import { AsyncPipe, NgOptimizedImage } from '@angular/common';
import { AuthService } from '../../../auth/data/services/auth.service';
import { OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { RouterLink } from '@angular/router';
import { WishlistService } from '../../data/services/wishlist.service';
import { IWishlist } from '../../../../core/models/wishlist';

@Component({
  selector: 'app-wishlist',
  standalone: true,
  imports: [WishlistItemsComponent, NgOptimizedImage, AsyncPipe, RouterLink],
  templateUrl: './wishlist.component.html',
  styleUrl: './wishlist.component.css',
})
export class WishlistComponent implements OnInit {
  authService = inject(AuthService);
  isLoggedIn$: Observable<boolean> | undefined;
  wishlist!: IWishlist;
  wishlistService = inject(WishlistService);
  wishlist$!: Observable<IWishlist>;

  getWishlist() {
    return this.wishlistService
      .getWishlistFromLoggedUser()
      ?.subscribe((wishlist) => (this.wishlist = wishlist));
  }

  ngOnInit(): void {
    this.isLoggedIn$ = this.authService.isLoggedIn();
    this.wishlist$ = this.wishlistService.wishlist$;
    this.getWishlist();
  }
}
