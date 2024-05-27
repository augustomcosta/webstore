import { createSelector } from '@ngrx/store';

import { AppState } from '../../../../../app.store';
import { ShippingMethodSelectedState, ShippingState } from './shipping.reducer';

export const selectShippingState = (state: AppState) => state.shipping;
export const selectShippingMethodState = (state: AppState) =>
  state.shippingMethod;

export const selectFormData = createSelector(
  selectShippingState,
  (state: ShippingState) => state.formData,
);

export const selectShippingMethod = createSelector(
  selectShippingMethodState,
  (state: ShippingMethodSelectedState) => state.shippingMethod,
);
