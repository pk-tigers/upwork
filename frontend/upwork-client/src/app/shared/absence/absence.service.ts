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
      `${this.env.apiUrl}/absence/CreateAbsenceRequest`,
      absence
    ); //TODO: to be implemented on backend site
  }

  public getAbsencesForUser(
    userId: string,
    from: Date,
    to: Date,
    pageNumber = 0,
    pageSize = 10
  ): Observable<PaginatedResult<Absence>> {
    console.log('2');
    return this.http.get<PaginatedResult<Absence>>(
      `${
        this.env.apiUrl
      }/absences/GetAbsencesByUserId?userId=${userId}&from=${from.toISOString()}&to=${to.toISOString()}&skip=${
        pageNumber * pageSize
      }&take=${pageSize}`
    );
  }
}
