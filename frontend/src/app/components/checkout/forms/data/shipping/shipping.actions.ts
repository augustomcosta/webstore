import { createAction, props } from '@ngrx/store';
import { AddressVO } from '../../../../../core/models/address-vo';

export const submitForm = createAction('[Form] Submit Form');

export const submitFormSuccess = createAction(
  '[Form] Submit Success',
  props<{ formData: AddressVO }>(),
);

export const submitFormFailure = createAction(
  '[Form] Submit Failure',
  props<{ error: AddressVO }>(),
);
