import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../../core/models/product';
import { ProductService } from '../../../services/product.service';
import { ItemDetailsComponent } from '../../item-details/item-details.component';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [ItemDetailsComponent],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
})
export class ProductListComponent implements OnInit {
  selectedProduct: Product | undefined;
  productService = inject(ProductService);
  route = inject(ActivatedRoute);

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const productId = params['id'];
      this.productService.getProductById(productId).subscribe((product) => {
        this.selectedProduct = product;
      });
    });
  }
}
