using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IUsersService
    {
        public IEnumerable<UserModel> GetUsers(int skip, int take);
        public IEnumerable<UserModel> GetUsersByOrganizationId(Guid OrganizationId);
    }
}
