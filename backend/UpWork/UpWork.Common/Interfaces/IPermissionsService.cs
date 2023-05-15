using UpWork.Common.Dto;
using UpWork.Common.Enums;

namespace UpWork.Common.Interfaces
{
    public interface IPermissionsService
    {
        bool UpdatePermissions(UpdatePermissionsDto updatePermissionsDto);
        bool VerifyPermissionDatabase(Guid userId, PermissionType permType, Guid? organizationId);
    }
}
