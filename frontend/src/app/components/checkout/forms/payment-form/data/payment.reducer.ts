import { createReducer, on } from '@ngrx/store';

import { PaymentMethod } from '../../../../../core/models/payment-method';
import {
  submitForm,
  submitFormFailure,
  submitFormSuccess,
} from './payment.actions';

export interface PaymentState {
  formData: PaymentMethod;
  submitting: boolean;
  error: any;
}

const initialState: PaymentState = {
  formData: new PaymentMethod(),
  submitting: false,
  error: null,
};

export const paymentReducer = createReducer(
  initialState,
  on(submitForm, (state) => ({ ...state, submitting: true })),
  on(submitFormSuccess, (state, { formData }) => ({
    ...state,
    formData,
    submitting: false,
  })),
  on(submitFormFailure, (state, { error }) => ({
    ...state,
    submitting: false,
    error,
  })),
);
