import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { IProduct } from '../core/models/IProduct';
import { ProductParams } from '../shared/models/productParams';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;
  products: IProduct[] = [];
  productParams = new ProductParams();

  constructor() {}

  getProducts() {
    return this.http.get<IProduct[]>(
      this.apiUrl +
        `/Product/pagination?PageNumber=${this.productParams.pageNumber}&PageSize=${this.productParams.pageSize}&api-version=1`,
    );
  }

  getProductById(id: string | null) {
    const product = this.products.find((p) => p.id === id);
    if (product) {
      return of(product);
    }

    return this.http.get<IProduct>(this.apiUrl + `/Product/${id}`);
  }
}
