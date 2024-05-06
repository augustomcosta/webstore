import { createSelector } from '@ngrx/store';
import { selectShippingState, ShippingState } from './shipping.reducer';

export const selectFormData = createSelector(
  selectShippingState,
  (state: ShippingState) => state.formData,
);
