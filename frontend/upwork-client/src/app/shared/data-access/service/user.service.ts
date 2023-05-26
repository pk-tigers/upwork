import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { User } from '../../../models/user.model';
import { TokenService } from './token.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoginModel } from '../../../models/login.model';
import { AuthenticatedResponse } from '../../../models/authenticated-response.model';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { OrganizationService } from './organization.service';
import { UpdateUserDto } from 'src/app/models/update-user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private user = new BehaviorSubject<User | null>(null);
  public user$ = this.user.asObservable();
  private isAdmin = new BehaviorSubject<boolean>(false);
  public isAdmin$ = this.isAdmin.asObservable();

  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
    private jwtHelper: JwtHelperService,
    private organizationService: OrganizationService
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

  public getUser(id: String): Observable<User>{
    return this.http.get<User>(`${environment.apiUrl}/User/${id}`);
  }

  public login(loginModel: LoginModel): Observable<boolean> {
    return this.http
      .post<AuthenticatedResponse>(`${environment.apiUrl}/token`, loginModel)
      .pipe(
        map((res: AuthenticatedResponse) => {
          if (!res) return false;
          this.tokenService.setToken(res);
          this.setUser(res);
          return true;
        })
      );
  }

  public logout(): void {
    this.tokenService.clearToken();
    this.clearUser();
  }

  private setUser(auth: AuthenticatedResponse | null): void {
    if (!auth) return;
    const decodeToken = this.jwtHelper.decodeToken(auth.token);
    const user: User = {
      id: decodeToken['userId'],
      organizationId: decodeToken['organizationId'],
      roles: this.getRoles(decodeToken['permissions']),
    };
    this.user.next(user);
    this.organizationService.initOrganization(user);
    if (decodeToken['admin']) this.isAdmin.next(true);
  }

  private getRoles(roles: string | string[] | undefined): string[] {
    if (typeof roles === 'undefined') return [];
    if (typeof roles === 'string') return [roles];
    return roles;
  }

  private clearUser() {
    this.user.next(null);
    this.isAdmin.next(false);
    this.organizationService.clearOrganization();
  }

  public updateUser(id: string, organizationId: string,updateUserDto: UpdateUserDto) {
    return this.http.put<boolean>(
      `${environment.apiUrl}/User/${id}?organizationId=${organizationId}`,
      updateUserDto
    );
  }
}
