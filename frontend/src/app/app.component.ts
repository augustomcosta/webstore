import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  inject,
  OnInit,
} from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AsyncPipe } from '@angular/common';
import { SideBarComponent } from './components/shared/side-bar/side-bar.component';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HomeComponent,
    AsyncPipe,
    RouterLink,
    SideBarComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppComponent implements OnInit {
  title = 'Web Store';
  private authService = inject(AuthService);

  ngOnInit(): void {
    this.setCurrentUser();
  }

  private setCurrentUser() {
    const userToken = localStorage.getItem('user');
    const userName = localStorage.getItem('userName');
  }
}
