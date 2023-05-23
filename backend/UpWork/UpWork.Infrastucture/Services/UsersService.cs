using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using UpWork.Common.Dto;
using UpWork.Common.Enums;
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

        public PaginatedResult<UserModel> GetSupervisors(Guid organizationId, int skip, int take)
        {
            var users = _context.Users
                .Where(x => x.IsActive)
                .Include(x => x.Permissions)
                .Where(x => x.Role == Role.OrganizationOwner || x.Permissions.Any(z => z.PermissionType == PermissionType.CanSupervise 
                && z.GrantDate < DateTime.UtcNow && (z.ExpirationDate == null || z.ExpirationDate > DateTime.UtcNow)));

            var res = new PaginatedResult<UserModel>(users.Skip(skip).Take(take), users.Count(), take);
            return res;
        }

        public PaginatedResult<UserModel> GetUsers(int skip, int take)
        {
            var users = _context.Users.Where(x => x.IsActive);

            var res = new PaginatedResult<UserModel>(users.Skip(skip).Take(take), users.Count(), take);
            return res;
        }

        public PaginatedResult<UserModel> GetUsersByOrganizationId(Guid OrganizationId, int skip, int take)
        {
            var users = _context.Users.Where(x => x.OrganizationId == OrganizationId && x.IsActive);

            var res = new PaginatedResult<UserModel>(users.Skip(skip).Take(take), users.Count(), take);
            return res;
        }

        public PaginatedResult<UserModel> GetOwnersByOrganizationId(Guid OrganizationId, int skip, int take)
        {
            var users = _context.Users.Where(x => x.OrganizationId == OrganizationId && x.Role == Role.OrganizationOwner && x.IsActive);

            var res = new PaginatedResult<UserModel>(users.Skip(skip).Take(take), users.Count(), take);
            return res;
        }

        public PaginatedResult<UserWithPermissionsDto> LoadUsersWithPermissions(Guid organizationId, int skip, int take)
        {
            var users = _context.Users
                .Where(x => x.OrganizationId == organizationId && x.IsActive)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Include(x => x.Permissions)
                .Select(x => MapUserWithPermissionsFromUserModel(x));

            var res = new PaginatedResult<UserWithPermissionsDto>(users.Skip(skip).Take(take), users.Count(), take);
            return res;
        }

        public PaginatedResult<UserWithSupervisorDto> UsersWithSupervisors(Guid organizationId, int skip, int take)
        {
            var users = _context.Users.Where(x => x.OrganizationId == organizationId && x.IsActive).Include(x => x.CurrentTimeOffSupervisor)
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

        private static UserWithPermissionsDto MapUserWithPermissionsFromUserModel(UserModel userModel)
        {
            var res = new UserWithPermissionsDto()
            {
                Id = userModel.Id,
                OrganizationId = userModel.OrganizationId,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Role = userModel.Role,
                Email = userModel.Email
            };

            var currentPermissions = userModel.Permissions
                .Where(x => x.IsActive())
                .Select(x => x.PermissionType);

            res.PermissionTypes = currentPermissions;

            return res;
        }
    }
}
