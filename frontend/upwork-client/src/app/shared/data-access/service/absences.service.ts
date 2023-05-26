import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Absence } from 'src/app/models/absence.model';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { environment } from 'src/environments/environment';
import { DatePipe } from '@angular/common';
@Injectable({
  providedIn: 'root',
})
export class AbsencesService {
  private env = environment;
  constructor(private http: HttpClient, private datePipe: DatePipe) {}

  public getAbsencesByOrganizationId(
    organizationId: string | undefined,
    fromDate: Date,
    toDate: Date,
    skip = 0,
    take = 10
  ): Observable<PaginatedResult<Absence>> {
    const fromDateFormatted = this.datePipe.transform(fromDate, 'yyyy-MM-dd');
    const toDateFormatted = this.datePipe.transform(toDate, 'yyyy-MM-dd');

    return this.http.get<PaginatedResult<Absence>>(
      `${this.env.apiUrl}/Absences/GetAbsencesByOrganizationId?organizationId=${organizationId}&from=${fromDateFormatted}&to=${toDateFormatted}&skip=${skip}&take=${take}`
    );
  }

  public getAbsencesForUser(
    fromDate: Date,
    toDate: Date,
    skip = 0,
    take = 10
  ): Observable<PaginatedResult<Absence>> {
    const fromDateFormatted = this.datePipe.transform(fromDate, 'yyyy-MM-dd');
    const toDateFormatted = this.datePipe.transform(toDate, 'yyyy-MM-dd');
    return this.http.get<PaginatedResult<Absence>>(
      `${
        this.env.apiUrl
      }/Absences/GetAbsencesByDateForUser?from=${fromDateFormatted}&to=${toDateFormatted}&skip=${
        skip * take
      }&take=${take}`
    );
  }
}
