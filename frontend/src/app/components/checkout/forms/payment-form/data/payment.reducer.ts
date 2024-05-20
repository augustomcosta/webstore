import { createReducer, on } from '@ngrx/store';
import { resetPaymentState, submitPayment } from './payment.actions';
import { state } from '@angular/animations';

export interface PaymentState {
  value: string;
}

const initialState: PaymentState = {
  value: '',
};

export const paymentReducer = createReducer(
  initialState,
  on(submitPayment, (state, { payment }) => ({
    ...state,
    value: payment,
  })),
  on(resetPaymentState, (state) => ({
    ...state,
    state: initialState,
    value: initialState.value,
  })),
);
