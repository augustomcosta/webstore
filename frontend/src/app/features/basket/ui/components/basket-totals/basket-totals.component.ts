import { Component, inject, Input, OnInit } from '@angular/core';
import { Basket } from '../../../../../core/models/basket';
import { BasketService } from '../../../data/services/basket.service';
import { CurrencyPipe } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-basket-totals',
  standalone: true,
  imports: [CurrencyPipe, RouterLink],
  templateUrl: './basket-totals.component.html',
  styleUrl: './basket-totals.component.css',
})
export class BasketTotalsComponent implements OnInit {
  basketService = inject(BasketService);
  @Input() basket!: Basket;
  subTotal = this.calculateSubTotal();
  totals: number = this.calculateTotals();

  calculateSubTotal(): number {
    return this.basketService.calculateTotals();
  }

  calculateTotals(): number {
    return this.subTotal;
  }

  ngOnInit(): void {
    this.calculateTotals();
    this.calculateSubTotal();
  }
}
