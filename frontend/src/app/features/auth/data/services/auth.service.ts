import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../../../environments/environment';
import { LoginRequest } from '../interfaces/login-request';
import {BehaviorSubject, catchError, Observable, throwError} from 'rxjs';
import { AuthResponse } from '../interfaces/auth-response';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { BasketService } from '../../../basket/data/services/basket.service';
import { RegisterRequest } from '../interfaces/register-request';
import { RegisterResponse } from '../interfaces/register-response';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl: string = environment.apiUrl;
  router = inject(Router);
  registerSource = new BehaviorSubject<boolean>(false);
  register$ = this.registerSource.asObservable();
  basketService = inject(BasketService);
  private loggedUser = new BehaviorSubject<string>('');
  private tokenKey = 'token';
  private loggedInSource = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {}

  register(data: RegisterRequest): Observable<RegisterResponse> {
    return this.http
      .post<RegisterResponse>(
        `${this.apiUrl}/Auth/register?api-version=1`,
        data,
      )
      .pipe(
        map((response) => {
          if (response.isSuccess) {
            this.registerSource.next(true);
          }
          return response;
        }),
      );
  }

  login(data: LoginRequest): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${this.apiUrl}/Auth/login?api-version=1`, data)
      .pipe(
        map((response) => {
          if (response.isSuccess) {
            localStorage.setItem(this.tokenKey, response.token);

            localStorage.setItem('basket_id', response.basketId);

            localStorage.setItem('userId', response.userId);

            localStorage.setItem('wishlistId', response.wishlistId);

            this.loggedInSource.next(true);

            this.router.navigate(['/']);

            this.setCurrentUser(response.userName);
          }
          return response;
        }),
        catchError(this.handleError)
      );
  }

  private handleError(err: HttpErrorResponse): Observable<never> {
    return throwError(() => err.error);
  }


  getLoggedUser(): Observable<string> {
    const userName = localStorage.getItem('userName');

    this.loggedUser.next(userName!);

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
    localStorage.removeItem('wishlist_id');
    this.loggedInSource.next(false);
    this.router.navigateByUrl('/');
  }

  setCurrentUser(userName: string) {
    localStorage.setItem('userName', userName);
    this.loggedUser.next(userName);
  }
}
