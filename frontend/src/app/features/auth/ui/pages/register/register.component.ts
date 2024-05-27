import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule, ValidationErrors, ValidatorFn,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../../data/services/auth.service';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, AsyncPipe, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit, OnDestroy {
  authService = inject(AuthService);
  hide = true;
  form!: FormGroup;
  fb = inject(FormBuilder);
  register$: Observable<boolean> | undefined;

  register() {
    return this.authService.register(this.form.value).subscribe();
  }

  passwordValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value;

      if (!value) {
        return null;
      }

      const hasUppercase = /[A-Z]/.test(value);
      const hasNumber = /\d/.test(value);
      const hasSpecial = /[!@#$%^&*(),.?":{}|<>]/.test(value);

      const passwordValid = hasUppercase && hasNumber && hasSpecial;

      return !passwordValid ? {passwordStrength: true} : null;
    };
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', [Validators.required, this.passwordValidator()]],
    });
    this.register$ = this.authService.register$;
  }

  ngOnDestroy(): void {
    this.authService.registerSource.next(false);
  }
}
