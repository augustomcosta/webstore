import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { IPaymentMethod } from '../core/models/payment-method';
import { BehaviorSubject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PaymentMethodService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;
  paymentMethodsSource = new BehaviorSubject<IPaymentMethod[]>(null as any);
  paymentMethods$ = this.paymentMethodsSource.asObservable();

  constructor() {}

  getPaymentMethods() {
    return this.http
      .get<IPaymentMethod[]>(this.apiUrl + `/PaymentMethod/get-payment-methods`)
      .pipe(
        tap((paymentMethods: IPaymentMethod[]) => {
          this.paymentMethodsSource.next(paymentMethods);
        }),
      );
  }
}
