import {Component} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {RouterLink} from "@angular/router";

@Component({
  selector: 'item-card-app',
  templateUrl: 'item-card.component.html',
  styleUrl: 'item-card.component.css',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, RouterLink],
})
export class ItemCardComponent {}
