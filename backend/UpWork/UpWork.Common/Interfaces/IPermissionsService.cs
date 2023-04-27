using UpWork.Common.Enums;

namespace UpWork.Common.Interfaces
{
    public interface IPermissionsService
    {
        bool VerifyPermissionDatabase(Guid userId, PermissionType permType, Guid? organizationId);
    }
}
