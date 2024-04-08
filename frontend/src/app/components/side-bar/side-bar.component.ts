import { Component, inject, OnInit } from '@angular/core';
import { AsyncPipe, NgIf } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { Observable } from 'rxjs';
import { MatSlideToggle } from '@angular/material/slide-toggle';
import { ItemCardComponent } from '../item-card/item-card.component';
import {
  MatError,
  MatFormField,
  MatFormFieldModule,
  MatLabel,
} from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { InputClearableExample } from '../search-bar/search-bar.component';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.css',
  standalone: true,
  imports: [
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    AsyncPipe,
    MatSlideToggle,
    ItemCardComponent,
    MatLabel,
    MatFormField,
    MatError,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    InputClearableExample,
    RouterModule,
    NgIf,
  ],
})
export class SideBarComponent implements OnInit {
  isLoggedIn$: Observable<boolean> | undefined;
  authService = inject(AuthService);
  loggedUser$: Observable<string> | undefined;
  userName: string | undefined;

  constructor() {}

  logout() {
    this.authService.logout();
  }

  isLoggedIn(): Observable<boolean> {
    return this.authService.isLoggedIn();
  }

  getLoggedUser() {
    return this.authService.getLoggedUser();
  }

  toggleDropdown() {
    const dropUp = document.getElementById('drop-up-content');
    // @ts-ignore
    if (dropUp.style.display === 'block') {
      // @ts-ignore
      dropUp.style.display = 'none';
    }
    // @ts-ignore
    dropUp.style.display = 'block';
  }

  ngOnInit(): void {
    this.isLoggedIn$ = this.isLoggedIn();
    this.loggedUser$ = this.getLoggedUser();
    this.loggedUser$.subscribe((userName) => {
      this.userName = userName;
    });
  }
}
