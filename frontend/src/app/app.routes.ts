import { Routes } from '@angular/router';
import {BasketComponent} from "./components/basket/basket.component";
import {HomeComponent} from "./components/home/home.component";
import {ItemDetailsComponent} from "./components/item-details/item-details.component";

export const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'home', component: HomeComponent},
  {path: 'basket', component: BasketComponent},
  {path: 'item-details', component:ItemDetailsComponent}
];
