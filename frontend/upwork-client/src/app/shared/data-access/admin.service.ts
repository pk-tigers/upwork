import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of, tap } from 'rxjs';
import { OrganizationModel } from 'src/app/models/organization.model';
import { PaginatedResult } from 'src/app/models/paginatedResult.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private env = environment;
  constructor(private http: HttpClient) {}

  public createOrganization(
    organization: OrganizationModel
  ): Observable<OrganizationModel> {
    return this.http.post<OrganizationModel>(
      `${this.env.apiUrl}/organization`,
      organization
    );
  }

  public getOrganizations(
    pageNumber = 0,
    pageSize = 10
  ): Observable<PaginatedResult<OrganizationModel>> {
    return this.http.get<PaginatedResult<OrganizationModel>>(
      `${this.env.apiUrl}/organizations?skip=${
        pageNumber * pageSize
      }&take=${pageSize}`
    );
  }

  public deleteOrganization(guid: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.env.apiUrl}/organization/${guid}`);
  }
}
