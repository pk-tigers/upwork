import { PermissionTypes } from './enums/permission-types.enum';

export interface UpdatePermissions {
  userId: string;
  permissionTypes: PermissionTypes[];
}
