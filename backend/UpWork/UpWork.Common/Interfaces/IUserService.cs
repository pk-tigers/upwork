using UpWork.Common.Dto;
using UpWork.Common.Enums;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IUserService
    {
        UserModel CreateUser(RegisterDto registerDto, Role role = Role.User);
        UserModel GetUser(Guid Id);
        UserModel GetUser(Guid Id, Guid organizationId);
        bool DeleteUser(Guid Id);
        UserModel UpdateUser(UserModel existingUser, UpdateUserDto updateUserDto);
        bool UpdateUserSupervisor(UpdateUserSupervisorDto updateUserSupervisor);
    }
}
