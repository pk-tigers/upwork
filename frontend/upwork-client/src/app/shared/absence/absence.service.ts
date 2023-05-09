import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AbsenceModel } from 'src/app/models/absence.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AbsenceService {
  private env = environment;
  constructor(private http: HttpClient) {}

  public createAbsenceRequest(absence: AbsenceModel): Observable<AbsenceModel> {
    return this.http.post<AbsenceModel>(`${this.env.apiUrl}/absence`, absence);
  }

  public getAbsencesForUser() {
    //TODO
  }
}
