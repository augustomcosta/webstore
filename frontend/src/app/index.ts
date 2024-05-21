import {
  checkoutReducer,
  CheckoutState,
  orderReducer,
  OrderSubmitState,
} from './components/checkout/data/checkout.reducer';
import {
  ActionReducer,
  ActionReducerMap,
  combineReducers,
  MetaReducer,
} from '@ngrx/store';
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
  order: OrderSubmitState;
}

export const reducers: ActionReducerMap<AppState> = {
  checkout: checkoutReducer,
  shipping: shippingReducer,
  payment: paymentReducer,
  shippingMethod: shippingMethodReducer,
  order: orderReducer,
};

type PartialAppState = Partial<AppState>;

export function getInitialAppState(): PartialAppState {
  const previousCheckoutSettings = localStorage.getItem('checkout');
  const previousShippingSettings = localStorage.getItem('shipping');
  const previousPaymentSettings = localStorage.getItem('payment');
  const previousShippingMethodSettings = localStorage.getItem('shippingMethod');

  const initialState: PartialAppState = {};

  if (previousCheckoutSettings != null) {
    initialState.checkout = JSON.parse(previousCheckoutSettings);
  }

  if (previousShippingSettings != null) {
    initialState.shipping = JSON.parse(previousShippingSettings);
  }

  if (previousPaymentSettings != null) {
    initialState.payment = JSON.parse(previousPaymentSettings);
  }

  if (previousShippingMethodSettings != null) {
    initialState.shippingMethod = JSON.parse(previousShippingMethodSettings);
  }

  return initialState;
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

export function resetStateMetaReducer(
  reducer: ActionReducer<AppState>,
): ActionReducer<AppState> {
  return (state, action) => {
    if (action.type === '[Order] Place Order Success') {
      state = undefined;
    }
    return reducer(state, action);
  };
}

export const metaReducers: MetaReducer<AppState, any>[] = [
  localStorageSyncReducer,
  resetStateMetaReducer,
];

export const effects = [ShippingEffects];
