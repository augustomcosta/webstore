import { OrderItemVO } from './order-item';
import { DeliveryMethod } from './delivery-method';
import { AddressVO } from './address-vo';

export interface Order {
  id: string;
  subTotal: number;
  buyerEmail: string;
  orderItems: OrderItemVO[];
  totalItemQuantity: number;
  orderDate: string;
  deliveryMethod: DeliveryMethod;
  shippingAddress: AddressVO;
  deliveryMethodId: string;
  total: number;
  userId: string;
}
