import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import {
  saveShippingMethod,
  submitForm,
  submitFormFailure,
  submitFormSuccess,
} from './shipping.actions';
import { catchError, of, switchMap, tap } from 'rxjs';
import { UserService } from '../../../../../services/user.service';
import { map } from 'rxjs/operators';
import { AddressVO } from '../../../../../core/models/address-vo';
import { IDeliveryMethod } from '../../../../../core/models/delivery-method';

@Injectable()
export class ShippingEffects {
  userService = inject(UserService);
  address = new AddressVO();

  submitShippingForm$ = createEffect(() =>
    this.actions$.pipe(
      ofType(submitForm),
      switchMap(() =>
        this.userService.getUserAddress().pipe(
          map((formData) => submitFormSuccess({ formData })),
          catchError((error) => of(submitFormFailure({ error }))),
        ),
      ),
    ),
  );

  updateDeliveryMethod$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(saveShippingMethod), // Listen for the saveShippingMethod action
        tap((action) => {
          const selectedOptionId = action.shippingMethod.id; // Assuming your DeliveryMethod has an id property
          const deliveryMethodSelect = document.getElementById(
            'deliveryMethod',
          ) as HTMLSelectElement;
          if (deliveryMethodSelect) {
            // Find the option with the corresponding id and set it as selected
            for (let i = 0; i < deliveryMethodSelect.options.length; i++) {
              const option = deliveryMethodSelect.options[
                i
              ] as HTMLOptionElement;
              const optionValue: IDeliveryMethod = JSON.parse(option.value);
              if (optionValue.id === selectedOptionId) {
                option.selected = true;
                break;
              }
            }
          }
        }),
      ),
    { dispatch: false }, // We don't need to dispatch any actions in this effect
  );

  constructor(private actions$: Actions) {}
}
