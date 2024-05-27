import { Component, inject, OnInit } from '@angular/core';
import { AsyncPipe, CurrencyPipe, NgOptimizedImage } from '@angular/common';
import { BasketSummaryComponent } from '../../../basket/ui/components/basket-summary/basket-summary.component';
import { BasketTotalsComponent } from '../../../basket/ui/components/basket-totals/basket-totals.component';
import { RouterLink } from '@angular/router';
import { OrderService } from '../../data/services/order.service';
import { Observable } from 'rxjs';
import { Order } from '../../domain/models/order';
import { OrderDetailsComponent } from '../components/order-details/order-details.component';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    AsyncPipe,
    BasketSummaryComponent,
    BasketTotalsComponent,
    RouterLink,
    OrderDetailsComponent,
    CurrencyPipe,
    NgOptimizedImage,
  ],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css',
})
export class OrdersComponent implements OnInit {
  orderService = inject(OrderService);
  userOrders$!: Observable<Order[]>;
  orders!: Order[];

  ngOnInit(): void {
    this.orderService.getAllOrdersForUser().subscribe((orders) => {
      this.orders = orders;
    });
    this.userOrders$ = this.orderService.userOrders$;
  }
}
