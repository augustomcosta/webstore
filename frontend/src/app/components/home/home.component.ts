import { Component, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import {RouterModule} from "@angular/router";
import {ItemCardComponent} from "../item-card/item-card.component";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  standalone: true,
  imports: [
    AsyncPipe,
    RouterModule,
    ItemCardComponent
  ]
})
export class HomeComponent {

}
