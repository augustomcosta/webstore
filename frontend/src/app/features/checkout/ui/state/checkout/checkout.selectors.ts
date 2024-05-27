import { createSelector } from '@ngrx/store';
import { CheckoutState } from './checkout.reducer';
import { AppState } from '../../../../../index';

export const selectCheckout = (state: AppState) => state.checkout;

export const selectStepperStep = createSelector(
  selectCheckout,
  (state: CheckoutState) => state.stepperStep,
);
