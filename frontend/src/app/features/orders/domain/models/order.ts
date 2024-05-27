import { OrderItemVO } from './order-item';
import { DeliveryMethod } from '../../../checkout/domain/models/delivery-method';
import { AddressVO } from '../../../shared/value_objects/address-vo';

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
