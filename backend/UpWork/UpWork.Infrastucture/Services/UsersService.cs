using Microsoft.EntityFrameworkCore.Query;
using UpWork.Common.Interfaces;
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

        public IEnumerable<UserModel> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public IEnumerable<UserModel> GetUsersByOrganizationId(Guid OrganizationId)
        {
            var users = _context.Users.Where(x => x.OrganizationId == OrganizationId);
            return users;
        }

    }
}
