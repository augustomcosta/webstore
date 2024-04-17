import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { ICategory } from '../core/models/category';
import { BehaviorSubject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;
  categoriesSource = new BehaviorSubject<ICategory[]>(null as any);
  categories$ = this.categoriesSource.asObservable();

  constructor() {}

  getCategories() {
    return this.http.get<ICategory[]>(this.apiUrl + `/Category`).pipe(
      tap((categories: ICategory[]) => {
        this.categoriesSource.next(categories);
      }),
    );
  }
}
