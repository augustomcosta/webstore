import {
  checkoutReducer,
  CheckoutState,
} from './components/checkout/data/checkout.reducer';
import { ActionReducerMap } from '@ngrx/store';
import {
  shippingReducer,
  ShippingState,
} from './components/checkout/forms/data/shipping/shipping.reducer';
import { ShippingEffects } from './components/checkout/forms/data/shipping/shipping.effects';

export interface AppState {
  checkout: CheckoutState;
  shipping: ShippingState;
}

export const reducers: ActionReducerMap<AppState> = {
  checkout: checkoutReducer,
  shipping: shippingReducer,
};

export function getInitialAppState() {
  const previousSettings = localStorage.getItem('checkout');
  if (previousSettings != null) {
    return JSON.parse(previousSettings);
  }
  return {};
}

export const effects = [ShippingEffects];
