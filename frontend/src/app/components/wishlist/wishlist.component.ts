import { Component, inject } from '@angular/core';
import { WishlistItemComponent } from './wishlist-item/wishlist-item.component';
import { AsyncPipe, NgOptimizedImage } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-wishlist',
  standalone: true,
  imports: [WishlistItemComponent, NgOptimizedImage, AsyncPipe, RouterLink],
  templateUrl: './wishlist.component.html',
  styleUrl: './wishlist.component.css',
})
export class WishlistComponent implements OnInit {
  authService = inject(AuthService);
  isLoggedIn$: Observable<boolean> | undefined;

  ngOnInit(): void {
    this.isLoggedIn$ = this.authService.isLoggedIn();
  }
}
