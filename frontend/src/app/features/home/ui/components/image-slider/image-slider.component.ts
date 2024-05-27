import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  inject,
  OnInit,
  Output,
} from '@angular/core';
import {
  NgClass,
  NgForOf,
  NgIf,
  NgOptimizedImage,
  NgStyle,
} from '@angular/common';
import { CategoryService } from '../../../data/services/category.service';
import { Category } from '../../../domain/models/category';
import { Observable } from 'rxjs';
import { RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-image-slider',
  standalone: true,
  imports: [
    NgOptimizedImage,
    NgForOf,
    NgStyle,
    NgIf,
    NgClass,
    RouterLinkActive,
  ],
  templateUrl: './image-slider.component.html',
  styleUrl: './image-slider.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ImageSliderComponent implements OnInit {
  @Output() categorySelected = new EventEmitter<string>();
  activeCategory: string | undefined;
  categoryService = inject(CategoryService);
  categories: Category[] = [];
  categories$: Observable<Category[]> | undefined;

  getCategories() {
    this.categoryService.getCategories().subscribe((categories) => {
      this.categories = categories;
    });
  }

  selectCategory(category: string) {
    this.categorySelected.emit(category);
    this.activeCategory = category;
  }

  isActiveCategory(category: string): boolean {
    return this.activeCategory === category;
  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.categories$;
    this.getCategories();
  }
}
