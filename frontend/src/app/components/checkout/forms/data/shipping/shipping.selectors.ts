import { createSelector } from '@ngrx/store';

import { AppState } from '../../../../../index';
import { ShippingState } from './shipping.reducer';

export const selectShippingState = (state: AppState) => state.shipping;

export const selectFormData = createSelector(
  selectShippingState,
  (state: ShippingState) => state.formData,
);
