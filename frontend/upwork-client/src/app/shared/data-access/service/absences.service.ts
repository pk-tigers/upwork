import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Absence } from 'src/app/models/absence.model';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { RegisterModel } from 'src/app/models/register.model';
import { User } from 'src/app/models/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AbsencesService {
  private env = environment;
  constructor(private http: HttpClient) { }

  public getAbsencesByOrganizationId(
    organizationId : string | undefined,
    fromDate : string,
    toDate : string,
    skip=0,
    take=10
  ): Observable<PaginatedResult<Absence>> {
    return this.http.get<PaginatedResult<Absence>>(
      `${this.env.apiUrl}/Absences/GetAbsencesByOrganizationId?organizationId=${organizationId}&from=${fromDate}&to=${toDate}&skip=${skip}&take=${take}`
    );
  }

  public getAbsencesForUser(
    fromDate : string,
    toDate : string,
    skip=0,
    take=10
  ): Observable<PaginatedResult<Absence>> {
    return this.http.get<PaginatedResult<Absence>>(
      `${this.env.apiUrl}/Absences/GetAbsencesByDateForUser?from=${fromDate}&to=${toDate}&skip=${skip*take}&take=${take}`
    );
  }
}


