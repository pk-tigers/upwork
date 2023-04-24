using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UpWork.Common.DTO;
using UpWork.Common.Identity;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Infrastucture.Services
{
    public class TokenService : ITokenService
    {


        private readonly ApplicationDbContext _dbContext;
        private readonly IEncodeService _encodeService;
        private readonly IConfiguration _configuration;

        public TokenService(ApplicationDbContext dbContext, IEncodeService encodeService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _encodeService = encodeService;
            _configuration = configuration;
        }
        public bool TryAuthenticateUser(LoginDto loginData, out string token)
        {
            token = String.Empty;
            try
            {
                if (loginData == null || string.IsNullOrEmpty(loginData.Email) || string.IsNullOrEmpty(loginData.Password))
                    return false;

                var userModel = _dbContext.Users.Where(x => x.Email == loginData.Email).FirstOrDefault();

                if (userModel is null)
                    return false;

                if (!_encodeService.VerifyUser(userModel.Password, loginData.Password))
                    return false;

                token = CreateToken(userModel);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string CreateToken(UserModel userModel)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);
            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: GetUserClaims(userModel),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        private List<Claim> GetUserClaims(UserModel user)
        {
            var claims = new List<Claim>();
            if (user.Role == Common.Enums.Role.Admin)
                claims.Add(new Claim(IdentityData.AdminUserClaimName, "true"));
            return claims;
        }
    }
}
