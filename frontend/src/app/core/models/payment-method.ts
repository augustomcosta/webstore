export interface IPaymentMethod {
  name: string;
}

export class PaymentMethod implements IPaymentMethod {
  name: string = '';
}
