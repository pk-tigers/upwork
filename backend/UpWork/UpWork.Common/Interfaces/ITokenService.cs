using UpWork.Common.Dto;

namespace UpWork.Common.Interfaces
{
    public interface ITokenService
    {
        public bool TryAuthenticateUser(LoginDto loginData, out string token);
    }
}
