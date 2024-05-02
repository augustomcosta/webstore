import {
  checkoutReducer,
  CheckoutState,
} from './components/checkout/data/checkout.reducer';
import { ActionReducerMap } from '@ngrx/store';

export interface AppState {
  checkout: CheckoutState;
}

export const reducers: ActionReducerMap<AppState> = {
  checkout: checkoutReducer,
};
