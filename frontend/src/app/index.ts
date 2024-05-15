import {
  checkoutReducer,
  CheckoutState,
} from './components/checkout/data/checkout.reducer';
import { ActionReducer, ActionReducerMap, MetaReducer } from '@ngrx/store';
import {
  shippingMethodReducer,
  ShippingMethodSelectedState,
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
  shippingMethod: ShippingMethodSelectedState;
}

export const reducers: ActionReducerMap<AppState> = {
  checkout: checkoutReducer,
  shipping: shippingReducer,
  payment: paymentReducer,
  shippingMethod: shippingMethodReducer,
};

export function getInitialAppState() {
  const previousCheckoutSettings = localStorage.getItem('checkout');

  const previousShippingSettings = localStorage.getItem('shipping');

  const previousPaymentSettings = localStorage.getItem('payment');

  const previousShippingMethodSettings = localStorage.getItem('shippingMethod');

  if (previousCheckoutSettings != null) {
    return JSON.parse(previousCheckoutSettings);
  }

  if (previousShippingSettings != null) {
    return JSON.parse(previousShippingSettings);
  }

  if (previousPaymentSettings != null) {
    return JSON.parse(previousPaymentSettings);
  }

  if (previousShippingMethodSettings != null) {
    return JSON.parse(previousShippingMethodSettings);
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
      { shippingMethod: ['shippingMethod'] },
    ],
    rehydrate: true,
  })(reducer);
}

export const metaReducers: MetaReducer<AppState, any>[] = [
  localStorageSyncReducer,
];

export const effects = [ShippingEffects];
