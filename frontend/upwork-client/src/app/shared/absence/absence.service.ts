import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Absence } from 'src/app/models/absence.model';
import { Observable } from 'rxjs';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';

@Injectable({
  providedIn: 'root',
})
export class AbsenceService {
  private env = environment;
  constructor(private http: HttpClient) {}

  public createAbsenceRequest(absence: Absence): Observable<Absence> {
    return this.http.post<Absence>(
      `${this.env.apiUrl}/Absence/CreateAbsenceRequestForUser`,
      absence
    );
  }

  public getAbsencesForUser(
    pageNumber = 0,
    pageSize = 10
  ): Observable<PaginatedResult<Absence>> {
    return this.http.get<PaginatedResult<Absence>>(
      `${this.env.apiUrl}/Absences/GetAbsencesForUser?skip=${
        pageNumber * pageSize
      }&take=${pageSize}`
    );
  }

  public cancelRequest(requestId: string): Observable<boolean> {
    return this.http.delete<boolean>(
      `${this.env.apiUrl}/Absence/CancelRequestForUser/${requestId}`
    );
  }

  public getYearAbsenceCountForUser(): Observable<number> {
    return this.http.get<number>(
      `${this.env.apiUrl}/Absences/getYearAbsenceCountForUser`
    );
  }
}
