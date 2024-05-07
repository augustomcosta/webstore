import { createReducer, on } from '@ngrx/store';
import { AddressVO } from '../../../../../core/models/address-vo';
import {
  submitForm,
  submitFormFailure,
  submitFormSuccess,
} from './shipping.actions';

export interface ShippingState {
  formData: AddressVO;
  submitting: boolean;
  error: any;
}

const initialState: ShippingState = {
  formData: new AddressVO(),
  submitting: false,
  error: null,
};

export const shippingReducer = createReducer(
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
