using UpWork.Common.Enums;

namespace UpWork.Common.Dto
{
    public class UserWithPermissionsDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public IEnumerable<PermissionType> PermissionTypes { get; set; }
    }
}
