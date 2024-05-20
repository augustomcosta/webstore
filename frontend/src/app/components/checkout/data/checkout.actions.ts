import { createAction } from '@ngrx/store';

export const nextStep = createAction('[Checkout Component] Next Step');
export const previousStep = createAction('[Checkout Component] Previous Step');
export const placeOrderSuccess = createAction('[Order] Place Order Success');
export const resetCheckoutState = createAction(
  '[Checkout Component] Reset State',
);
