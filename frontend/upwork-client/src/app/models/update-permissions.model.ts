import { PermissionTypes } from './enums/permission-types.enum';

export interface UpdatePermissions {
  userId: string;
  organizationId: string | null;
  permissionTypes: PermissionTypes[];
}
