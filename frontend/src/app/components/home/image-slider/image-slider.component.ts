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
import { CategoryService } from '../../../services/category.service';
import { ICategory } from '../../../core/models/category';
import { SwiperDirective } from '../../../directives/swiper-directive';
import { Observable } from 'rxjs';
import { RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-image-slider',
  standalone: true,
  imports: [
    NgOptimizedImage,
    NgForOf,
    NgStyle,
    SwiperDirective,
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
  categories: ICategory[] = [];
  categories$: Observable<ICategory[]> | undefined;

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
