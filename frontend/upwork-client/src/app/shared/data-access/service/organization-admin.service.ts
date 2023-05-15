import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, switchMap } from 'rxjs';
import { OrganizationModel } from 'src/app/models/organization.model';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { RegisterModel } from 'src/app/models/register.model';
import { UserWithSupervisor } from 'src/app/models/user-with-supervisor.model';
import { User } from 'src/app/models/user.model';
import { environment } from 'src/environments/environment';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root',
})
export class OrganizationAdminService {
  private env = environment;
  private organizationId;


  constructor(
    private http: HttpClient,
    private userService: UserService,
    ) {
        this.organizationId = this.getOrganizationId();
    }


   
    public getOrganizationId(): Observable<string> {
        return this.userService.user$.pipe(
          map(user => user?.organizationId || '') // Jeżeli user lub organizationId nie istnieje, zwraca pustą wartość
        );
      }


    /*
    public AddUser(user: User): Observable<User> {
        return this.userService.user$.pipe(
          switchMap(userObj => {
            if (!userObj?.organizationId) {
              throw new Error('User organization ID not found');
            }
            user.organizationId = userObj.organizationId;
            return this.http.post<User>(
              `${this.env.apiUrl}/user`,
              user
            );
          })
        );
      }
      */

  /*

  public getUsersWithSupervisors(
    pageNumber = 0,
    pageSize = 10,
    guid: string
  ): Observable<PaginatedResult<UserWithSupervisor>> {
    return this.http.get<PaginatedResult<UserWithSupervisor>>(
        `${this.env.apiUrl}/users/userswithsupervisors?orgId=${guid}&skip=${
            pageNumber * pageSize
      }&take=${pageSize}`
    );
  }

*/


public getUsersWithSupervisors(pageNumber = 0, pageSize = 10): Observable<PaginatedResult<UserWithSupervisor>> {
    return this.userService.user$.pipe(
      switchMap(user => {
        if (!user?.organizationId) {
          throw new Error('User organization ID not found');
        }
        return this.http.get<PaginatedResult<UserWithSupervisor>>(
          `${this.env.apiUrl}/users/userswithsupervisors?orgId=${this.organizationId}&skip=${pageNumber * pageSize}&take=${pageSize}`
        );
      })
    );
  }
  

  public deleteUser(guid: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.env.apiUrl}/user/${guid}`);
  }

  public addUser(user: RegisterModel): Observable<User> {
    return this.http.post<User>(`${this.env.apiUrl}/user`, user);
  }


}


