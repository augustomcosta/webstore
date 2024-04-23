import { Component } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { BasketSummaryComponent } from '../basket/basket-summary/basket-summary.component';
import { BasketTotalsComponent } from '../basket/basket-totals/basket-totals.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    AsyncPipe,
    BasketSummaryComponent,
    BasketTotalsComponent,
    RouterLink,
  ],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css',
})
export class OrdersComponent {}
