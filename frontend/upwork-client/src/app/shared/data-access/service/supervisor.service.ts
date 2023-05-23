import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, switchMap } from 'rxjs';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { UpdateSupervisor } from 'src/app/models/update-supervisor.model';
import { User } from 'src/app/models/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})

export class SupervisorService {
  private env = environment;

  constructor(private http: HttpClient) {}

  public getSupervisors(orgId?: string): Observable<PaginatedResult<User>> {
    return this.http.get<PaginatedResult<User>>(
      `${this.env.apiUrl}/users/getsupervisors?organizationId=${orgId}`
    );
  }
  
  public updateUserSupervisor(updateSupervisor: UpdateSupervisor): Observable<UpdateSupervisor> {
    return this.http.post<UpdateSupervisor>(`${this.env.apiUrl}/user/updateUserSupervisor`, updateSupervisor);
}

}