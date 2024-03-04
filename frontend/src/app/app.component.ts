import {Component, CUSTOM_ELEMENTS_SCHEMA} from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import {HomeComponent} from "./components/home/home.component";
import {AsyncPipe} from "@angular/common";
import {SideBarComponent} from "./components/side-bar/side-bar.component";
import {ItemDetailsComponent} from "./components/item-details/item-details.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HomeComponent, AsyncPipe, RouterLink, SideBarComponent,ItemDetailsComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  schemas : [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppComponent {
  title = 'frontend';

}
