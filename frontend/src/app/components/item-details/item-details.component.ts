import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { IProduct } from '../../core/models/IProduct';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { BasketSummaryComponent } from '../basket-summary/basket-summary.component';
import { BasketTotalsComponent } from '../basket-totals/basket-totals.component';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-item-details',
  standalone: true,
  imports: [
    BasketSummaryComponent,
    BasketTotalsComponent,
    RouterLink,
    NgOptimizedImage,
  ],
  templateUrl: './item-details.component.html',
  styleUrl: './item-details.component.css',
})
export class ItemDetailsComponent implements OnInit {
  productService = inject(ProductService);
  // @ts-ignore
  product: IProduct;
  activatedRoute = inject(ActivatedRoute);

  // @ts-ignore

  getProductById() {
    const productId = this.activatedRoute.snapshot.paramMap.get('id');
    this.productService.getProductById(productId).subscribe((product) => {
      this.product = product;
    });
  }

  ngOnInit(): void {
    this.getProductById();
  }
}
