import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { BehaviorSubject, tap } from 'rxjs';
import { IDeliveryMethod } from '../../domain/models/delivery-method';

@Injectable({
  providedIn: 'root',
})
export class DeliveryMethodService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;
  deliveryMethodsSource = new BehaviorSubject<IDeliveryMethod[]>(null as any);
  deliveryMethods$ = this.deliveryMethodsSource.asObservable();

  constructor() {}

  getDeliveryMethods() {
    return this.http
      .get<IDeliveryMethod[]>(this.apiUrl + `/DeliveryMethod`)
      .pipe(
        tap((deliveryMethods: IDeliveryMethod[]) => {
          this.deliveryMethodsSource.next(deliveryMethods);
        }),
      );
  }
}
