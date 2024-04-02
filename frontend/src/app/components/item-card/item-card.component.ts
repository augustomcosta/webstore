import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { RouterLink } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from '../../core/models/product';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'item-card-app',
  templateUrl: 'item-card.component.html',
  styleUrl: 'item-card.component.css',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, RouterLink, NgOptimizedImage],
})
export class ItemCardComponent implements OnInit {
  productService = inject(ProductService);
  products: Product[] = [];
  protected readonly Math = Math;

  getProducts() {
    this.productService.getProducts().subscribe((products) => {
      this.products = products;
    });
  }

  ngOnInit(): void {
    this.getProducts();
  }
}
