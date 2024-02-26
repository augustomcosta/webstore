import {inject, Injectable, signal} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  #http = inject(HttpClient);
  #url = signal(environment.apiUrl);

}
