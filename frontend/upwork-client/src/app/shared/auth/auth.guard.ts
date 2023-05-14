import { inject } from '@angular/core';
import { UserService } from '../data-access/service/user.service';
import { Router } from '@angular/router';

export const authGuard = async () => {
  const userService = inject(UserService);
  const router = inject(Router);

  if (!userService.isUserAuthenticated) {
    await router.navigateByUrl('/login');
    return false;
  }

  return true;
};
