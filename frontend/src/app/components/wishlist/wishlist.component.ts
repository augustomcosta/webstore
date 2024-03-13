import { Component } from '@angular/core';
import {WishlistItemComponent} from "../wishlist-item/wishlist-item.component";
import {NgOptimizedImage} from "@angular/common";

@Component({
  selector: 'app-wishlist',
  standalone: true,
  imports: [
    WishlistItemComponent,
    NgOptimizedImage
  ],
  templateUrl: './wishlist.component.html',
  styleUrl: './wishlist.component.css'
})
export class WishlistComponent {

}
