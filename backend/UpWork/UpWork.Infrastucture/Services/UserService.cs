﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpWork.Common.DTO;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Infrastucture.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEncodeService _encodeService;

        public UserService(ApplicationDbContext context, IEncodeService encodeService)
        {
            _context = context;
            _encodeService = encodeService;
        }

        public UserModel CreateUser(RegisterDto registerDto)
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
                Role = Common.Enums.Role.User,
                Password = _encodeService.EncodePassword(generatedPassword)
            };

            _context.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }
    }
}