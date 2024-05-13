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
  const previousSettings = localStorage.getItem('checkout');
  if (previousSettings != null) {
    return JSON.parse(previousSettings);
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
      { payment: ['formData', 'submitting', 'error'] },
    ],
    rehydrate: true,
  })(reducer);
}

export const metaReducers: MetaReducer<AppState, any>[] = [
  localStorageSyncReducer,
];

export const effects = [ShippingEffects];
