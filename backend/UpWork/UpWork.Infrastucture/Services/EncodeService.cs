using System.Text;
using UpWork.Common.Interfaces;

namespace UpWork.Infrastucture.Services
{
    public class EncodeService : IEncodeService
    {
        public byte[] EncodePassword(string password)
        {
            return Encoding.UTF8.GetBytes(BCrypt.Net.BCrypt.HashPassword(password));
        }

        public bool VerifyUser(byte[] userPassword, string loginPassword)
        {
            return BCrypt.Net.BCrypt.Verify(loginPassword, Encoding.UTF8.GetString(userPassword));
        }
    }
}
