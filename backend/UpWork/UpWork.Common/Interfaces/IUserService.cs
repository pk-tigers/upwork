using UpWork.Common.Dto;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IUserService
    {
        UserModel CreateUser(RegisterDto registerDto);
        UserModel GetUser(Guid Id);
        bool DeleteUser(Guid Id);
        
    }
}
