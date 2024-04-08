import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  inject,
  OnInit,
} from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ItemCardComponent } from '../item-card/item-card.component';
import { ImageSliderComponent } from '../image-slider/image-slider.component';
import { ProductService } from '../../services/product.service';
import { BasketService } from '../../services/basket.service';
import { IProduct } from '../../core/models/IProduct';

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
  basketService = inject(BasketService);
  products: IProduct[] = [];
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
