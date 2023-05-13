using UpWork.Common.Enums;

namespace UpWork.Common.Dto
{
    public class UpdatePermissionsDto
    {
        public Guid UserId { get; set; }
        public List<PermissionType> PermissionTypes { get; set; }
    }
}
