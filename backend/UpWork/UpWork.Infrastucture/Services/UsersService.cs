using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using UpWork.Common.Dto;
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
            var users = _context.Users;

            var res = new PaginatedResult<UserModel>(users.Skip(skip).Take(take), users.Count(), take);
            return res;
        }

        public PaginatedResult<UserModel> GetUsersByOrganizationId(Guid OrganizationId, int skip, int take)
        {
            var users = _context.Users.Where(x => x.OrganizationId == OrganizationId);

            var res = new PaginatedResult<UserModel>(users.Skip(skip).Take(take), users.Count(), take);
            return res;
        }

        public PaginatedResult<UserWithSupervisorDto> UsersWithSupervisors(Guid organizationId, int skip, int take)
        {
            var users = _context.Users.Where(x => x.OrganizationId == organizationId).Include(x => x.CurrentTimeOffSupervisor)
                .Select(x => new UserWithSupervisorDto()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SupervisorFirstName = x.CurrentTimeOffSupervisor != null ? x.CurrentTimeOffSupervisor.FirstName : null,
                    SupervisorLastName = x.CurrentTimeOffSupervisor != null ? x.CurrentTimeOffSupervisor.LastName : null,
                }); ;

            var res = new PaginatedResult<UserWithSupervisorDto>(users.Skip(skip).Take(take), users.Count(), take);
            return res;
        }
    }
}
