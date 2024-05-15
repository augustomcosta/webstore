import { createReducer, on } from '@ngrx/store';
import { AddressVO } from '../../../../../core/models/address-vo';
import {
  saveShippingMethod,
  submitForm,
  submitFormFailure,
  submitFormSuccess,
} from './shipping.actions';
import {
  DeliveryMethod,
  IDeliveryMethod,
} from '../../../../../core/models/delivery-method';

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

export interface ShippingMethodSelectedState {
  value: IDeliveryMethod;
}

const shippingMethodInitialState: ShippingMethodSelectedState = {
  value: new DeliveryMethod(),
};

export const shippingMethodReducer = createReducer(
  shippingMethodInitialState,
  on(
    saveShippingMethod,
    (state: ShippingMethodSelectedState, { shippingMethod }) => ({
      ...state,
      shippingMethod,
    }),
  ),
);

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
