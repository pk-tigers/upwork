using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpWork.Common.Dto;
using UpWork.Common.Enums;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Infrastucture.Services
{
    public class PermissionsService : IPermissionsService
    {
        private readonly ApplicationDbContext _context;

        public PermissionsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool UpdatePermissions(UpdatePermissionsDto updatePermissionsDto)
        {
            var user = _context.Users
                .Where(x => x.Id == updatePermissionsDto.UserId)
                .Where(x => x.OrganizationId == updatePermissionsDto.OrganizationId)
                .Include(x => x.Permissions)
                .FirstOrDefault();

            if (user == null) return false;

            var permissionsToRemove = user.Permissions
                .Where(x => x.IsActive())
                .Where(x => !updatePermissionsDto.PermissionTypes.Contains(x.PermissionType));
            foreach (var perm in permissionsToRemove)
            {
                perm.ExpirationDate = DateTime.UtcNow;
            }

            var permissionsToAdd = updatePermissionsDto.PermissionTypes
                .Where(x => !user.Permissions.Any(z => z.IsActive() && z.PermissionType == x));
            foreach (var permType in permissionsToAdd)
            {
                var newPermission = new PermissionModel() { 
                    PermissionType = permType, 
                    UserId = user.Id, 
                    GrantDate = DateTime.UtcNow
                };
                user.Permissions.Add(newPermission);
            }

            _context.SaveChanges();

            return true;
        }

        public bool VerifyPermissionDatabase(Guid userId, PermissionType permType, Guid? organizationId)
        {
            try
            {
                var userPermData = _context.Users.Where(x => x.Id == userId).Select(x => new { x.Role, x.Permissions }).First();
                if (userPermData.Role is Role.PageAdmin) return true;

                if (organizationId is null)
                    throw new UnauthorizedAccessException();

                var permission = userPermData.Permissions
                    .Where(x => x.PermissionType == permType 
                    && x.IsActive());

                if (permission is null)
                    throw new UnauthorizedAccessException();

                return true;
            }
            catch
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
