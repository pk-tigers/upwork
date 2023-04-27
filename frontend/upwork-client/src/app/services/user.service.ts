import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { User } from '../models/user.model';
import { TokenService } from './token.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoginModel } from '../models/login.model';
import { AuthenticatedResponse } from '../models/authenticated-response.model';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private user = new BehaviorSubject<User | null>(null);
  public user$ = this.user.asObservable();

  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
    private jwtHelper: JwtHelperService
  ) {
    const token = this.tokenService.getToken();
    if (!!token && !this.jwtHelper.isTokenExpired(token))
      this.setUser({ token: token } as AuthenticatedResponse);
    else this.tokenService.clearToken();
  }

  public get isUserAuthenticated(): boolean {
    const token = this.tokenService.getToken();
    if (!!token && !this.jwtHelper.isTokenExpired(token)) return true;
    return false;
  }

  public login(loginModel: LoginModel): Observable<boolean> {
    return this.http
      .post<AuthenticatedResponse>(`${environment.apiUrl}/token`, loginModel)
      .pipe(
        map((res: AuthenticatedResponse) => {
          if (!res) return false;
          this.setUser(res);
          this.tokenService.setToken(res);
          return true;
        })
      );
  }

  public logout(): void {
    this.tokenService.clearToken();
    this.setUser(null);
  }

  private setUser(auth: AuthenticatedResponse | null): void {
    if (!auth) return;
    const roles = this.getUserClams(auth);
    const user: User = {
      roles: roles,
    };
    this.user.next(user);
  }

  private getUserClams(auth: AuthenticatedResponse): string[] {
    const token: any = this.jwtHelper.decodeToken(auth.token);
    return []; // TODO: get user roles when added
  }
}
