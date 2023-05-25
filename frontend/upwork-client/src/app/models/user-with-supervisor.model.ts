import { Roles } from './enums/roles.enum';

export interface UserWithSupervisor {
  id?: string;
  firstName?: string;
  lastName?: string;
  supervisorFirstName?: string;
  supervisorLastName?: string;
}