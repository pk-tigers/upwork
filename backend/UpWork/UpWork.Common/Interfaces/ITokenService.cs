using UpWork.Common.DTO;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface ITokenService
    {
        public bool TryAuthenticateUser(LoginDto loginData, out string token);
    }
}
