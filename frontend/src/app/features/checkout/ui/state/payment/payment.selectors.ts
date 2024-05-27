import { AppState } from '../../../../../app.store';
import { createSelector } from '@ngrx/store';
import { PaymentState } from './payment.reducer';

export const selectPaymentState = (state: AppState) => state.payment;

export const selectPaymentData = createSelector(
  selectPaymentState,
  (state: PaymentState) => state.value,
);
