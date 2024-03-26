import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { LoginRequest } from '../interfaces/login-request';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthResponse } from '../interfaces/auth-response';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl: string = environment.apiUrl;
  private tokenKey = 'token';
  router = inject(Router);
  private loggedInSource = new BehaviorSubject<boolean>(false);

  loggedIn$ = this.loggedInSource.asObservable();

  constructor(private http: HttpClient) {}

  login(data: LoginRequest): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${this.apiUrl}/Auth/login?api-version=1`, data)
      .pipe(
        map((response) => {
          if (response.isSuccess) {
            localStorage.setItem(this.tokenKey, response.token);
            this.loggedInSource.next(true);
            this.router.navigate(['/']);
          }
          return response;
        }),
      );
  }

  logout(): void {
    localStorage.removeItem('token');
    this.loggedInSource.next(false);
    this.router.navigateByUrl('/');
  }
}
