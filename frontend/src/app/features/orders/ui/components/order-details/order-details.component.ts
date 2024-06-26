import { Component, Input, OnInit } from '@angular/core';
import { Order } from '../../../domain/models/order';
import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { WhitelabelIconComponent } from '../../../../shared/ui/components/whitelabel-icon/whitelabel-icon.component';

@Component({
  selector: 'app-order-details',
  standalone: true,
  imports: [CurrencyPipe, AsyncPipe, RouterLink, WhitelabelIconComponent],
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.css',
})
export class OrderDetailsComponent implements OnInit {
  @Input() order!: Order;

  ngOnInit(): void {}
}
