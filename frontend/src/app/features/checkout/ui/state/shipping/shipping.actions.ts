import { createAction, props } from '@ngrx/store';
import { AddressVO } from '../../../../shared/value-objects/address-vo';
import { DeliveryMethod } from '../../../domain/models/delivery-method';

export const submitForm = createAction('[Form] Submit Form');

export const submitFormSuccess = createAction(
  '[Form] Submit Success',
  props<{ formData: AddressVO }>(),
);

export const submitFormFailure = createAction(
  '[Form] Submit Failure',
  props<{ error: AddressVO }>(),
);

export const saveShippingMethod = createAction(
  '[Shipping] Save Method',
  props<{ shippingMethod: DeliveryMethod }>(),
);

export const resetShippingState = createAction('[Shipping] Reset State');

export const resetShippingMethodState = createAction(
  '[ShippingMethod] Reset State',
);
