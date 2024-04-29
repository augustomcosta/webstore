export interface IAddressVO {
  street: string;
  neighborhood: string;
  city: string;
  state: string;
  zipCode: string;
  number: string;
}

export class AddressVO implements IAddressVO {
  city: string = '';
  neighborhood: string = '';
  number: string = '';
  state: string = '';
  street: string = '';
  zipCode: string = '';

  constructor() {}
}
