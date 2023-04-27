﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpWork.Common.Enums;
using UpWork.Common.Interfaces;
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

        public bool VerifyPermissionDatabase(Guid userId, PermissionType permType, Guid? organizationId)
        {
            try
            {
                var userPermData = _context.Users.Where(x => x.Id == userId).Select(x => new { x.Role, x.Permissions }).First();
                if (userPermData.Role is Role.Admin) return true;

                if (organizationId is null)
                    throw new UnauthorizedAccessException();

                var permission = userPermData.Permissions
                    .Where(x => x.PermissionType == permType 
                    && x.GrantDate < DateTime.UtcNow
                    && x.ExpirationDate.GetValueOrDefault(DateTime.MaxValue) > DateTime.UtcNow);

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