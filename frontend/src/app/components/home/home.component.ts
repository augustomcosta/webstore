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

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  standalone: true,
  imports: [AsyncPipe, RouterModule, ItemCardComponent, ImageSliderComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class HomeComponent {}
