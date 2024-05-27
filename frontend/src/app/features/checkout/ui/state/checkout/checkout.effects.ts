import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { placeOrderSuccess, resetCheckoutState } from './checkout.actions';
import { map } from 'rxjs/operators';
import { Store } from '@ngrx/store';
import {
  resetShippingMethodState,
  resetShippingState,
} from '../shipping/shipping.actions';
import { resetPaymentState } from '../payment/payment.actions';

@Injectable()
export class CheckoutEffects {
  store = inject(Store);

  submitOrder$ = createEffect(() =>
    this.actions$.pipe(
      ofType(placeOrderSuccess),
      map(() => {
        this.store.dispatch(resetCheckoutState());
        this.store.dispatch(resetShippingState());
        this.store.dispatch(resetPaymentState());
        this.store.dispatch(resetShippingMethodState());
        return { type: '[Checkout] Order Submitted and State Reset' };
      }),
    ),
  );

  constructor(private actions$: Actions) {}
}
