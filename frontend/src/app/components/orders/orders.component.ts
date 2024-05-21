import { Component, inject, OnInit } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { BasketSummaryComponent } from '../basket/basket-summary/basket-summary.component';
import { BasketTotalsComponent } from '../basket/basket-totals/basket-totals.component';
import { RouterLink } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { Observable } from 'rxjs';
import { Order } from '../../core/models/order';

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
export class OrdersComponent implements OnInit {
  orderService = inject(OrderService);
  userOrders$!: Observable<Order[]>;

  ngOnInit(): void {
    this.orderService.getAllOrdersForUser();
    this.userOrders$ = this.orderService.userOrders$;
  }
}
