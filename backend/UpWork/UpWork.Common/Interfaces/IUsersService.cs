using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IUsersService
    {
        public IEnumerable<UserModel> GetUsers();
        public IEnumerable<UserModel> GetUsersByOrganizationId(Guid OrganizationId);
    }
}
