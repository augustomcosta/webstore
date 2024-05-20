import { OrderItemVO } from './order-item';
import { DeliveryMethod } from './delivery-method';
import { AddressVO } from './address-vo';

export interface Order {
  subTotal: number;
  buyerEmail: string;
  orderItems: OrderItemVO[];
  deliveryMethod: DeliveryMethod;
  shippingAddress: AddressVO;
  deliveryMethodId: string;
  total: number;
  userId: string;
}
