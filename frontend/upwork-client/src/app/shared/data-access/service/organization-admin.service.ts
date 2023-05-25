import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { RegisterModel } from 'src/app/models/register.model';
import { UserWithSupervisor } from 'src/app/models/user-with-supervisor.model';
import { User } from 'src/app/models/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class OrganizationAdminService {
  private env = environment;

  constructor(private http: HttpClient) {}

  public getUsersWithSupervisors(
    organizationId?: string,
    pageNumber = 0,
    pageSize = 10
  ): Observable<PaginatedResult<UserWithSupervisor>> {
    return this.http.get<PaginatedResult<UserWithSupervisor>>(
      `${
        this.env.apiUrl
      }/users/UsersWithSupervisors?organizationId=${organizationId}&skip=${
        pageNumber * pageSize
      }&take=${pageSize}`
    );
  }

  public deleteUser(guid: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.env.apiUrl}/user/${guid}`);
  }

  public addUser(user: RegisterModel): Observable<User> {
    return this.http.post<User>(`${this.env.apiUrl}/user`, user);
  }
}
