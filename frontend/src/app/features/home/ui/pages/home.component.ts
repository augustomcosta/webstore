import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  inject,
  OnInit,
} from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ItemCardComponent } from '../components/item-card/item-card.component';
import { ImageSliderComponent } from '../components/image-slider/image-slider.component';
import { ProductService } from '../../../../services/product.service';
import { Product } from '../../../../core/models/product';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  standalone: true,
  imports: [AsyncPipe, RouterModule, ItemCardComponent, ImageSliderComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class HomeComponent implements OnInit {
  productService = inject(ProductService);
  products: Product[] = [];
  originalProducts: Product[] = [];
  filteredProducts: Product[] = [];
  products$: BehaviorSubject<Product[]> = new BehaviorSubject<Product[]>([]);

  getProducts() {
    this.productService.getProducts().subscribe((products) => {
      this.originalProducts = products;
      this.products$.next(products);
    });
  }

  filterByCategory(category: string) {
    if (category === 'All Products') {
      this.filteredProducts = this.originalProducts;
    } else {
      this.filteredProducts = this.originalProducts.filter(
        (product) => product.categoryName === category,
      );
    }
    this.products$.next(this.filteredProducts);
  }

  ngOnInit(): void {
    this.getProducts();
    this.products$.subscribe((products) => {
      this.products = products;
      this.filteredProducts = [...this.products];
    });
  }
}
