import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginModel } from 'src/app/models/login.model';
import { TokenService } from 'src/app/services/token.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private tokenService: TokenService
  ) {
    if (this.userService.isUserAuthenticated) {
      // TODO: navigate to home page. No need to log again
    }
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) return;
    const credentials: LoginModel = {
      username: this.loginForm.controls['username'].value,
      password: this.loginForm.controls['password'].value,
    };
    this.userService.login(credentials).subscribe({
      next: res => {
        this.tokenService.setToken(res);
        // TODO: navigate to correct page
      },
      error: (error: HttpErrorResponse) => {
        if (error.status === 401) {
          // TODO: handle unauthorized request
        } else {
          // TODO: handle error
        }
      },
    });
  }
}
