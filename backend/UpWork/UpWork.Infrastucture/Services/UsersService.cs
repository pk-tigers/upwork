using Microsoft.EntityFrameworkCore.Query;
using UpWork.Common.Interfaces;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Infrastucture.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _context;

        public UsersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedResult<UserModel> GetUsers(int skip, int take)
        {
            var users = _context.Users.Skip(skip).Take(take);

            var res = new PaginatedResult<UserModel>(users.Skip(skip).Take(take), users.Count(), take);
            return res;
        }

        public PaginatedResult<UserModel> GetUsersByOrganizationId(Guid OrganizationId, int skip, int take)
        {
            var users = _context.Users.Where(x => x.OrganizationId == OrganizationId).Skip(skip).Take(take);

            var res = new PaginatedResult<UserModel>(users.Skip(skip).Take(take), users.Count(), take);
            return res;
        }

    }
}
