import { createAction, props } from '@ngrx/store';

export const submitPayment = createAction(
  '[Payment] Submit Payment',
  props<{ payment: string }>(),
);

export const resetPaymentState = createAction('[Payment] Reset State');
