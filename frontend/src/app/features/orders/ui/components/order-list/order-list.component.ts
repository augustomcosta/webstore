import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from '../../../data/services/order.service';
import { ItemDetailsComponent } from '../../../../shared/ui/components/item-details/item-details.component';
import { OrderDetailsComponent } from '../order-details/order-details.component';
import { Order } from '../../../domain/models/order';

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [ItemDetailsComponent, OrderDetailsComponent],
  templateUrl: './order-list.component.html',
  styleUrl: './order-list.component.css',
})
export class OrderListComponent implements OnInit {
  selectedOrder!: Order;
  orderService = inject(OrderService);
  route = inject(ActivatedRoute);

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const orderId = params['id'];
      this.orderService.getOrderById(orderId).subscribe((order) => {
        this.selectedOrder = order;
      });
    });
  }
}
