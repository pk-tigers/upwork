using UpWork.Common.DTO;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IUserService
    {
        UserModel CreateUser(RegisterDto registerDto);
        UserModel GetUser(Guid Id);
        void DeleteUser(Guid Id);
        
    }
}
