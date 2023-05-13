import { PermissionTypes } from './enums/permission-types.enum';
import { Roles } from './enums/roles.enum';

export interface UserWithPermissions {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  role: Roles;
  permissionTypes: PermissionTypes[];
}
