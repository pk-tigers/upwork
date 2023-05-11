﻿using UpWork.Common.Dto;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IUsersService
    {
        PaginatedResult<UserModel> GetUsers(int skip, int take);
        PaginatedResult<UserModel> GetUsersByOrganizationId(Guid OrganizationId, int skip, int take);
        PaginatedResult<UserWithSupervisorDto> UsersWithSupervisors(Guid organizationId, int skip, int take);
    }
}
