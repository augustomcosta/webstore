import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import {
  submitForm,
  submitFormFailure,
  submitFormSuccess,
} from './shipping.actions';
import { catchError, of, switchMap, tap } from 'rxjs';
import { UserService } from '../../../../../services/user.service';
import { map } from 'rxjs/operators';
import { AddressVO } from '../../../../../core/models/address-vo';

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

  constructor(private actions$: Actions) {}
}
