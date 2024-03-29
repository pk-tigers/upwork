﻿using UpWork.Common.Dto;
using UpWork.Common.Enums;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Infrastucture.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEncodeService _encodeService;
        private readonly IEmailService _emailService;

        public UserService(ApplicationDbContext context, IEncodeService encodeService, IEmailService emailService)
        {
            _context = context;
            _encodeService = encodeService;
            _emailService = emailService;
        }

        public UserModel CreateUser(RegisterDto registerDto, Role role = Role.User)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                string generatedPassword = _encodeService.GeneratePassword(16);

                var newUser = new UserModel()
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email,
                    IsActive = true,
                    CurrentTimeOffSupervisorId = registerDto.SupervisorId,
                    OrganizationId = registerDto.OrganizationId,
                    Role = role,
                    Password = _encodeService.EncodePassword(generatedPassword)
                };

                if (registerDto.OrganizationId.HasValue)
                {
                    newUser.Permissions = new List<PermissionModel>();
                    var basicPerm = new PermissionModel()
                    {
                        PermissionType = Common.Enums.PermissionType.BasicRead,
                        GrantDate = DateTime.UtcNow
                    };
                    newUser.Permissions.Add(basicPerm);
                }

                _context.Add(newUser);
                _context.SaveChanges();

                _emailService.SendEmail(newUser.Email, "Account created", CreateBodyForPasswordMessage(generatedPassword));

                transaction.Commit();
                return newUser;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public UserModel GetUser(Guid Id)
        {

            var user = _context.Users.FirstOrDefault(u => u.Id == Id);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public bool DeleteUser(Guid Id)
        {
            var user = _context.Users.Find(Id);

            if (user == null)
            {
                return false;
            }

            user.IsActive = false;
            _context.SaveChanges();

            return true;
        }


        private string CreateBodyForPasswordMessage(string pass)
        {
            string res = @"
<body>
	<h1>New Password Created</h1>
	<p>Your new password is: <strong>" + pass + @"</strong></p>
	<p>Please make sure to keep this password safe and do not share it with anyone.</p>
	<p>If this is a mistake please ignore this message.</p>
</body>";
            return res;
        }

        public UserModel UpdateUser(UserModel existingUser, UpdateUserDto updateUserDto)
        {


            if (!string.IsNullOrEmpty(updateUserDto.FirstName))
            {
                existingUser.FirstName = updateUserDto.FirstName;
            }

            if (!string.IsNullOrEmpty(updateUserDto.LastName))
            {
                existingUser.LastName = updateUserDto.LastName;
            }

            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                existingUser.Password = _encodeService.EncodePassword(updateUserDto.Password);
            }

            _context.SaveChanges();

            return existingUser;
        }

        public bool UpdateUserSupervisor(UpdateUserSupervisorDto updateUserSupervisor)
        {
            var user = _context.Users
                .Where(x => x.Id == updateUserSupervisor.UserId && x.OrganizationId == updateUserSupervisor.OrganizationId)
                .FirstOrDefault();

            if (user == null) return false;

            var newSupervisor = _context.Users
                .Where(x => x.Id == updateUserSupervisor.NewSupervisorId && x.OrganizationId == updateUserSupervisor.OrganizationId && x.IsActive)
                .FirstOrDefault();

            if (newSupervisor == null) return false;

            user.CurrentTimeOffSupervisorId = updateUserSupervisor.NewSupervisorId;

            _context.SaveChanges();

            return true;
        }
    }
}
