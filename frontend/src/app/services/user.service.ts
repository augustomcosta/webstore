import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { AddressVO } from '../core/models/address-vo';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;

  constructor() {}

  updateUserAddress(id: string, address: AddressVO) {
    return this.http.put<AddressVO>(
      this.apiUrl + `/User/update-user-address?id=${id}`,
      address,
    );
  }
}
