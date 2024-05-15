export interface IDeliveryMethod {
  id: string;
  name: string;
  deliveryTime: string;
  description: string;
  price: number;
}

export class DeliveryMethod implements IDeliveryMethod {
  deliveryTime = '';
  description = '';
  id = '';
  name = '';
  price = 0;

  constructor() {}
}
