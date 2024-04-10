import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { LoginRequest } from '../interfaces/login-request';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthResponse } from '../interfaces/auth-response';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { BasketService } from './basket.service';
import { RegisterRequest } from '../interfaces/register-request';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl: string = environment.apiUrl;
  router = inject(Router);
  basketService = inject(BasketService);
  private loggedUser = new BehaviorSubject<string>('');
  private loggedUser$ = this.loggedUser.asObservable();
  private tokenKey = 'token';
  private loggedInSource = new BehaviorSubject<boolean>(false);
  loggedIn$ = this.loggedInSource.asObservable();

  constructor(private http: HttpClient) {}

  register(data: RegisterRequest) {
    return this.http.post(`${this.apiUrl}/Auth/register?api-version=1`, data);
  }

  login(data: LoginRequest): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${this.apiUrl}/Auth/login?api-version=1`, data)
      .pipe(
        map((response) => {
          if (response.isSuccess) {
            localStorage.setItem(this.tokenKey, response.token);
            localStorage.setItem('userId', response.userId);
            this.loggedInSource.next(true);
            this.router.navigate(['/']);
            this.setCurrentUser(response.userName);
          }
          return response;
        }),
      );
  }

  getLoggedUser(): Observable<string> {
    const userName = localStorage.getItem('userName');
    // @ts-ignore
    this.loggedUser.next(userName);
    return this.loggedUser.asObservable();
  }

  isLoggedIn(): Observable<boolean> {
    if (!localStorage.getItem('token')) {
      this.loggedInSource.next(false);
      return this.loggedInSource.asObservable();
    }
    this.loggedInSource.next(true);
    return this.loggedInSource.asObservable();
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('userName');
    localStorage.removeItem('basket_id');
    this.loggedInSource.next(false);
    this.router.navigateByUrl('/');
  }

  setCurrentUser(userName: string) {
    localStorage.setItem('userName', userName);
    this.loggedUser.next(userName);
  }
}
