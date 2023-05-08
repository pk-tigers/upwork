import { inject } from '@angular/core';
import { UserService } from './user.service';
import { Router } from '@angular/router';
import { tap } from 'rxjs';

export const adminGuard = () => {
  const userService = inject(UserService);
  const router = inject(Router);

  return userService.isAdmin$.pipe(
    tap(async isAdmin => {
      if (!isAdmin) {
        await router.navigateByUrl('/');
        return false;
      }
      return true;
    })
  );
};
