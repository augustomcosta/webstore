import { Component, inject, Input, OnInit } from '@angular/core';
import { IBasket } from '../../../core/models/basket';
import { BasketService } from '../../../services/basket.service';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-basket-totals',
  standalone: true,
  imports: [CurrencyPipe],
  templateUrl: './basket-totals.component.html',
  styleUrl: './basket-totals.component.css',
})
export class BasketTotalsComponent implements OnInit {
  basketService = inject(BasketService);
  @Input() basket!: IBasket;
  subTotal = this.calculateSubTotal();
  totals: number = this.calculateTotals();

  calculateSubTotal(): number {
    return this.basketService.calculateTotals();
  }

  calculateTotals(): number {
    return this.subTotal + 10; //hardcoded for now
  }

  ngOnInit(): void {
    this.calculateTotals();
    this.calculateSubTotal();
  }
}
