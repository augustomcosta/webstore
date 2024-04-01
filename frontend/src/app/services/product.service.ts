import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { NgxPaginationModule } from 'ngx-pagination';
import { Product } from '../core/models/product';
import { ProductParams } from '../shared/models/productParams';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;
  products: Product[] = [];
  productParams = new ProductParams();

  constructor() {}

  getProducts() {
    return this.http.get<Product[]>(
      this.apiUrl +
        `/Product/pagination?PageNumber=${this.productParams.pageNumber}&PageSize=${this.productParams.pageSize}&api-version=1`,
    );
  }
}
