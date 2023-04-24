using UpWork.Common.Enums;

namespace UpWork.Common.Interfaces
{
    public interface IPermissionsService
    {
        bool VerifyPermission(Guid userId, PermissionType permType, Guid? organizationId);
    }
}
