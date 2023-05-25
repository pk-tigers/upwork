import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginModel } from 'src/app/models/login.model';
import { UserService } from 'src/app/shared/data-access/service/user.service';
import { OrganizationService } from 'src/app/shared/data-access/service/organization.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private organizationService: OrganizationService,
    private router: Router
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  async ngOnInit() {
    if (this.userService.isUserAuthenticated) {
      this.organizationService.organization$.subscribe(res => async () => {
        if (res?.urlName) {
          const url = res.urlName;
          await this.router.navigate([`/org/${url}/dashboard`]);
        }
      });
    }
  }

  onSubmit(): void {
    if (this.loginForm.invalid) return;
    const credentials: LoginModel = {
      email: this.loginForm.controls['email'].value,
      password: this.loginForm.controls['password'].value,
    };

    this.userService.login(credentials).subscribe(loggedIn => {
      (async () => {
        if (loggedIn) {
          this.organizationService.organization$.subscribe(res => {
            (async () => {
              if (res?.urlName) {
                const url = res.urlName;
                await this.router.navigate([`/org/${url}/dashboard`]);
              }
            })();
          });
        }
      })();
    });
  }
}
