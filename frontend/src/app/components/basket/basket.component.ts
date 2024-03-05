import { Component } from '@angular/core';
import {BasketSummaryComponent} from "../basket-summary/basket-summary.component";
import {BasketTotalsComponent} from "../basket-totals/basket-totals.component";

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [
    BasketSummaryComponent,
    BasketTotalsComponent
  ],
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.css'
})
export class BasketComponent {

}
