import { Routes } from '@angular/router';
import { BasketComponent } from './features/basket/ui/pages/basket.component';
import { HomeComponent } from './features/home/ui/pages/home.component';
import { WishlistComponent } from './features/wishlist/ui/pages/wishlist.component';
import { LoginComponent } from './features/auth/ui/pages/login/login.component';
import { RegisterComponent } from './features/auth/ui/pages/register/register.component';
import { ProductListComponent } from './features/shared/ui/components/product-list/product-list.component';
import { OrdersComponent } from './features/orders/ui/pages/orders.component';
import { CheckoutComponent } from './features/checkout/ui/pages/checkout.component';
import { OrderListComponent } from './features/orders/ui/components/order-list/order-list.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'basket', component: BasketComponent },
  { path: 'wishlist', component: WishlistComponent },
  { path: 'items/:id', component: ProductListComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'orders', component: OrdersComponent },
  { path: 'checkout', component: CheckoutComponent },
  { path: 'order/:id', component: OrderListComponent },
];
