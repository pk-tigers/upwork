import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AbsenceModel } from 'src/app/models/absence.model';
import { Observable } from 'rxjs';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';

@Injectable({
  providedIn: 'root',
})
export class AbsenceService {
  private env = environment;
  constructor(private http: HttpClient) {}

  public createAbsenceRequest(absence: AbsenceModel): Observable<AbsenceModel> {
    return this.http.post<AbsenceModel>(`${this.env.apiUrl}/absence`, absence); //TODO: to be implemented on backend site
  }

  public getAbsencesForUser(
    guid: string,
    from: Date,
    to: Date,
    pageNumber = 0,
    pageSize = 3
  ): Observable<PaginatedResult<AbsenceModel>> {
    console.log('2');
    return this.http.get<PaginatedResult<AbsenceModel>>(
      `${
        this.env.apiUrl
      }/absences/GetAbsencesByUserId/${guid}?from=${from.toISOString()}&to=${to.toISOString()}&skip=${
        pageNumber * pageSize
      }&take=${pageSize}`
    );
  }
}
