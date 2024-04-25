import { Component, inject, Input, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { BasketService } from '../../services/basket.service';
import { IBasket, IBasketTotals } from '../../core/models/basket';
import { Observable } from 'rxjs';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ICountries } from '../../shared/models/countries';
import { countries } from '../../shared/store/country-data-store';
import { AsyncPipe, CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [
    RouterLink,
    FormsModule,
    ReactiveFormsModule,
    AsyncPipe,
    CurrencyPipe,
  ],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css',
})
export class CheckoutComponent implements OnInit {
  basketService = inject(BasketService);
  basket$: Observable<IBasket> | undefined;
  countriesData: ICountries[] = countries;
  fb = inject(FormBuilder);
  form!: FormGroup;
  selectedCountry = '';
  basket: IBasket | undefined;
  basketTotal$: Observable<IBasketTotals> | undefined;

  constructor() {
    this.basket$ = this.basketService.basket$;
    this.basketService.getBasketFromLoggedUser().subscribe((basket) => {
      this.basket = basket;
    });

    this.basketTotal$ = this.basketService.basketTotal$;
  }

  onSelected(value: string): void {
    this.selectedCountry = value;
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      state: ['', Validators.required],
      city: ['', Validators.required],
      street: ['', Validators.required],
      country: [this.selectedCountry, Validators.required],
    });
  }
}
