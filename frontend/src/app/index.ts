import {
  checkoutReducer,
  CheckoutState,
} from './components/checkout/data/checkout.reducer';
import { ActionReducer, ActionReducerMap, MetaReducer } from '@ngrx/store';
import {
  shippingReducer,
  ShippingState,
} from './components/checkout/forms/data/shipping/shipping.reducer';
import { ShippingEffects } from './components/checkout/forms/data/shipping/shipping.effects';
import { localStorageSync } from 'ngrx-store-localstorage';
import {
  paymentReducer,
  PaymentState,
} from './components/checkout/forms/payment-form/data/payment.reducer';

export interface AppState {
  checkout: CheckoutState;
  shipping: ShippingState;
  payment: PaymentState;
}

export const reducers: ActionReducerMap<AppState> = {
  checkout: checkoutReducer,
  shipping: shippingReducer,
  payment: paymentReducer,
};

export function getInitialAppState() {
  const previousCheckoutSettings = localStorage.getItem('checkout');

  const previousShippingSettings = localStorage.getItem('shipping');

  const previousPaymentSettings = localStorage.getItem('payment');

  if (previousCheckoutSettings != null) {
    return JSON.parse(previousCheckoutSettings);
  }

  if (previousShippingSettings != null) {
    return JSON.parse(previousShippingSettings);
  }

  if (previousPaymentSettings != null) {
    return JSON.parse(previousPaymentSettings);
  }

  return {};
}

export function localStorageSyncReducer(
  reducer: ActionReducer<AppState>,
): ActionReducer<AppState> {
  return localStorageSync({
    keys: [
      { checkout: ['stepperStep'] },
      { shipping: ['formData', 'submitting', 'error'] },
      { payment: ['value'] },
    ],
    rehydrate: true,
  })(reducer);
}

export const metaReducers: MetaReducer<AppState, any>[] = [
  localStorageSyncReducer,
];

export const effects = [ShippingEffects];
