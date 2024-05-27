import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Category } from '../../domain/models/category';
import { BehaviorSubject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;
  categoriesSource = new BehaviorSubject<Category[]>(null as any);
  categories$ = this.categoriesSource.asObservable();

  constructor() {}

  getCategories() {
    return this.http.get<Category[]>(this.apiUrl + `/Category`).pipe(
      tap((categories: Category[]) => {
        this.categoriesSource.next(categories);
      }),
    );
  }
}
