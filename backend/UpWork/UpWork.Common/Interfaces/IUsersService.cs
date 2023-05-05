using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IUsersService
    {
        public PaginatedResult<UserModel> GetUsers(int skip, int take);
        public PaginatedResult<UserModel> GetUsersByOrganizationId(Guid OrganizationId, int skip, int take);
    }
}
