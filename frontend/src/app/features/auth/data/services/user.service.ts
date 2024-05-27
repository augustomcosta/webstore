import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../../environments/environment';
import { AddressVO } from '../../../shared/value_objects/address-vo';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  http = inject(HttpClient);
  apiUrl = environment.apiUrl;

  constructor() {}

  updateUserAddress(address: AddressVO) {
    const id = localStorage.getItem('userId');
    return this.http
      .put<AddressVO>(
        this.apiUrl + `/User/update-user-address?id=${id}`,
        address,
      )
      .subscribe();
  }

  getUserAddress() {
    const id = localStorage.getItem('userId');
    return this.http.get<AddressVO>(
      this.apiUrl + `/User/get-user-address?id=${id}`,
    );
  }
}
