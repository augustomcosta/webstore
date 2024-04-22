import { Component, inject, Input, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { IProduct } from '../../core/models/IProduct';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { BasketSummaryComponent } from '../basket/basket-summary/basket-summary.component';
import { BasketTotalsComponent } from '../basket/basket-totals/basket-totals.component';
import { CurrencyPipe, NgOptimizedImage } from '@angular/common';
import { BasketService } from '../../services/basket.service';
import { WishlistService } from '../../services/wishlist.service';
import { Observable } from 'rxjs';
import { IWishlist } from '../../core/models/wishlist';

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
  productService = inject(ProductService);
  @Input() product!: IProduct;
  wishlist!: IWishlist;
  activatedRoute = inject(ActivatedRoute);
  basketService = inject(BasketService);
  wishlistService = inject(WishlistService);
  wishlist$!: Observable<IWishlist>;

  constructor() {
    this.wishlist$ = this.wishlistService.wishlist$;
  }

  addItemToWishlist(item: IProduct) {
    this.wishlistService.addItemToWishlist(item);
  }

  addItemToBasket(item: IProduct) {
    this.basketService.addItemToBasket(item);
  }

  isItemOnWishlist(product: IProduct): boolean {
    return this.wishlistService.isItemOnWishlist(product);
  }

  getProductById() {
    const productId = this.activatedRoute.snapshot.paramMap.get('id');
    this.productService.getProductById(productId).subscribe((product) => {
      this.product = product;
    });
  }

  ngOnInit(): void {
    this.getProductById();
    this.wishlistService.getWishlistFromLoggedUser();
  }
}
