import { Component, inject, Input, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { BasketService } from '../../services/basket.service';
import { IBasket } from '../../core/models/basket';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css',
})
export class CheckoutComponent implements OnInit {
  basketService = inject(BasketService);
  basket: IBasket | undefined;
  basket$: Observable<IBasket> | undefined;

  getCurrentBasket() {
    this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });
  }

  constructor() {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.getCurrentBasket();
  }
}
