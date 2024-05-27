import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private toastr: ToastrService){}

  showNotification(message: string) {
    this.toastr.success(message);
  }
}
