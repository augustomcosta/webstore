import {AfterViewInit, Component, CUSTOM_ELEMENTS_SCHEMA, ElementRef, inject, OnInit, ViewChild} from '@angular/core';
import {NgForOf, NgIf, NgOptimizedImage, NgStyle} from "@angular/common";
import {CategoryService} from "../../../services/category.service";
import {ICategory} from "../../../core/models/category";
import { SwiperContainer } from 'swiper/element';
import { SwiperOptions } from 'swiper/types/swiper-options';
import {SwiperDirective} from "../../../directives/swiper-directive";
import {Observable} from "rxjs";

@Component({
  selector: 'app-image-slider',
  standalone: true,
  imports: [
    NgOptimizedImage,
    NgForOf,
    NgStyle,
    SwiperDirective,
    NgIf
  ],
  templateUrl: './image-slider.component.html',
  styleUrl: './image-slider.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ImageSliderComponent implements OnInit, AfterViewInit{
  @ViewChild('swiper') swiper!: ElementRef<SwiperContainer>;
  @ViewChild('swiperThumbs') swiperThumbs!: ElementRef<SwiperContainer>;
  index = 0;

  categoryService = inject(CategoryService);
  categories: ICategory[] = [];
  categories$: Observable<ICategory[]> | undefined;

  getCategories(){
    this.categoryService.getCategories().subscribe((categories) => {
      this.categories = categories;
    })
  }

  swiperConfig: SwiperOptions = {
    spaceBetween: 10,
    navigation: true,
  }

  swiperThumbsConfig: SwiperOptions = {
    spaceBetween: 10,
    slidesPerView: 4,
    freeMode: true,
    watchSlidesProgress: true,
  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.categories$;
    this.getCategories();
  }

  ngAfterViewInit(): void {
    this.swiper.nativeElement.swiper.activeIndex = this.index;
    this.swiperThumbs.nativeElement.swiper.activeIndex = this.index;
    this.swiper.nativeElement.addEventListener('slidechange', (evt) => this.slideChange(evt));
  }

  slideChange(swiper: any) {
    this.index = swiper.detail[0].activeIndex;
  }
}
