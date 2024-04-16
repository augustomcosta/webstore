import {AfterViewInit, Directive, ElementRef, EventEmitter, Input, Output} from '@angular/core';
import { SwiperContainer } from 'swiper/element';
import { SwiperOptions } from 'swiper/types';

@Directive({
  standalone: true,
  selector: '[appSwiper]'
})
export class SwiperDirective implements AfterViewInit {
  @Input() config?: SwiperOptions;
  @Output() slidechange = new EventEmitter<unknown>();

  constructor(private el: ElementRef<SwiperContainer>) { }

  ngAfterViewInit(): void {
    Object.assign(this.el.nativeElement, this.config);

    this.el.nativeElement.initialize();
  }
}
