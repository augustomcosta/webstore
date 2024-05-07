export interface IPaymentMethod {
  Id: string;
  Name: string;
}

export class PaymentMethod implements IPaymentMethod {
  Id = '';
  Name = '';
}
