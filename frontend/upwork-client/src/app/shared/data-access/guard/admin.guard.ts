import { inject } from '@angular/core';
import { UserService } from '../service/user.service';
import { Router } from '@angular/router';
import { map } from 'rxjs';

export const adminGuard = () => {
  const userService = inject(UserService);
  const router = inject(Router);

  return userService.isAdmin$.pipe(
    map(async isAdmin => {
      if (!isAdmin) await router.navigateByUrl('/');
      return isAdmin;
    })
  );
};
