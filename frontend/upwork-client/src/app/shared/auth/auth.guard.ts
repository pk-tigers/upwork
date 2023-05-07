import { inject } from '@angular/core';
import { UserService } from '../data-access/user.service';
import { Router } from '@angular/router';

export const authGuard = () => {
  const userService = inject(UserService);
  const router = inject(Router);

  if (!userService.isUserAuthenticated) {
    router.navigateByUrl('/login');
    return false;
  }

  return true;
};
