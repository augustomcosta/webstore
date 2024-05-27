import { createReducer, on } from '@ngrx/store';
import * as CheckoutActions from './checkout.actions';

export interface CheckoutState {
  stepperStep: number;
}

const initialState: CheckoutState = {
  stepperStep: 1,
};

export interface OrderSubmitState {
  isOrderSubmitted: boolean;
}

const orderInitialState: OrderSubmitState = {
  isOrderSubmitted: false,
};

export const checkoutReducer = createReducer(
  initialState,
  on(CheckoutActions.nextStep, (state) => ({
    ...state,
    stepperStep:
      state.stepperStep === 3 ? state.stepperStep : state.stepperStep + 1,
  })),
  on(CheckoutActions.previousStep, (state) => ({
    ...state,
    stepperStep:
      state.stepperStep > 1 ? state.stepperStep - 1 : state.stepperStep,
  })),
  on(CheckoutActions.resetCheckoutState, (state) => ({
    ...state,
    stepperStep: initialState.stepperStep,
  })),
);

export const orderReducer = createReducer(
  orderInitialState,
  on(CheckoutActions.placeOrderSuccess, (state) => ({
    ...state,
    isOrderSubmitted: true,
  })),
);
