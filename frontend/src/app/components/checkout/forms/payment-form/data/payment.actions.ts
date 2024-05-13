import { createAction, props } from '@ngrx/store';
import { IDeliveryMethod } from '../../../../../core/models/delivery-method';
import { PaymentMethod } from '../../../../../core/models/payment-method';

export const submitForm = createAction('[Form] Submit Form');

export const submitFormSuccess = createAction(
  '[Form] Submit Success',
  props<{ formData: PaymentMethod }>(),
);

export const submitFormFailure = createAction(
  '[Form] Submit Failure',
  props<{ error: PaymentMethod }>(),
);
