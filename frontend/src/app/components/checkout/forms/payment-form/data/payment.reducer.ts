import { createReducer, on } from '@ngrx/store';
import { submitPayment } from './payment.actions';

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
);
