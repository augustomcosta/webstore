import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import {NgOptimizedImage} from "@angular/common";

@Component({
  selector: 'app-image-slider',
  standalone: true,
  imports: [
    NgOptimizedImage
  ],
  templateUrl: './image-slider.component.html',
  styleUrl: './image-slider.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ImageSliderComponent {

}
