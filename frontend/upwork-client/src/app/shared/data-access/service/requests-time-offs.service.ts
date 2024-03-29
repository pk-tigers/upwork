import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AbsenceApprovalState } from 'src/app/models/absence-approval.state.model';
import { Absence } from 'src/app/models/absence.model';
import { ApprovalState } from 'src/app/models/enums/approval-state.enum';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class RequestsTimeOffsService {
  constructor(private http: HttpClient) {}

  public approveRequest(absenceId: string, approvalState: ApprovalState) {
    const data: AbsenceApprovalState = {
      absenceId: absenceId,
      approvalState: approvalState,
    };
    return this.http.post<Absence>(
      `${environment.apiUrl}/absence/SetAbsenceApprovalStateForSupervisor`,
      data
    );
  }

  public getListOfRequests(
    skip = 0,
    take = 10
  ): Observable<PaginatedResult<Absence>> {
    return this.http.get<PaginatedResult<Absence>>(
      `${
        environment.apiUrl
      }/absences/GetPendingAbsencesRequestsForSupervisor?skip=${
        skip * take
      }&take=${take}`
    );
  }

  public getListOfRequestsHistory(
    skip = 0,
    take = 10
  ): Observable<PaginatedResult<Absence>> | Observable<undefined> {
    return this.http.get<PaginatedResult<Absence>>(
      `${
        environment.apiUrl
      }/absences/GetSupervisedAbsencesRequestsForSupervisor?skip=${
        skip * take
      }&take=${take}`
    );
  }
}
