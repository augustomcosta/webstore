import { Component } from '@angular/core';
import {NgOptimizedImage} from "@angular/common";

@Component({
  selector: 'app-basket-summary',
  standalone: true,
  imports: [
    NgOptimizedImage
  ],
  templateUrl: './basket-summary.component.html',
  styleUrl: './basket-summary.component.css'
})
export class BasketSummaryComponent {

}
