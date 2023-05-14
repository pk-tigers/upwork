import { ActivatedRouteSnapshot, CanActivateFn } from '@angular/router';
import { map } from 'rxjs/operators';
import { OrganizationService } from '../service/organization.service';
import { inject } from '@angular/core';

export const organizationGuard: CanActivateFn = (
  activatedRoute: ActivatedRouteSnapshot
) => {
  const organizationService = inject(OrganizationService);
  const orgUrl = activatedRoute.paramMap.get('org-url');

  if (orgUrl == null) return false;
  return organizationService.getOrSetOrganizationByUrlName(orgUrl).pipe(
    map(res => {
      if (res) return true;
      return false;
    })
  );
};
