import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { IProduct } from '../../../core/models/IProduct';
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
  selectedProduct: IProduct | undefined;
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