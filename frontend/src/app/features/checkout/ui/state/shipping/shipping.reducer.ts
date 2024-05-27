import { createReducer, on } from '@ngrx/store';
import { AddressVO } from '../../../../shared/value-objects/address-vo';
import {
  resetShippingMethodState,
  resetShippingState,
  saveShippingMethod,
  submitForm,
  submitFormFailure,
  submitFormSuccess,
} from './shipping.actions';
import {
  DeliveryMethod,
  IDeliveryMethod,
} from '../../../domain/models/delivery-method';

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
  shippingMethod: IDeliveryMethod;
}

const shippingMethodInitialState: ShippingMethodSelectedState = {
  shippingMethod: new DeliveryMethod(),
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
  on(resetShippingState, (initialState) => ({
    ...initialState,
    formData: initialState.formData,
  })),
  on(resetShippingMethodState, (state) => ({
    ...state,
    state: shippingMethodInitialState,
    shippingMethod: shippingMethodInitialState.shippingMethod,
  })),
);
