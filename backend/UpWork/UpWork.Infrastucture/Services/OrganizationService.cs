using Microsoft.EntityFrameworkCore;
using UpWork.Common.Dto;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Infrastucture.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public OrganizationService(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService=userService;
        }

        public OrganizationModel CreateOrganization(CreateOrganizationDto organizationDTO)
        {
            var newOrganization = new OrganizationModel()
            {
                Name = organizationDTO.Name,
                UrlName= organizationDTO.UrlName
            };
            
            _context.Add(newOrganization);
            _context.SaveChanges();

            return newOrganization;
        }

        public OrganizationModel GetOrganizationWithUsers(Guid Id)
        {
            var organization = _context.Organizations
                .Include(o => o.Users)
                .FirstOrDefault(x => x.Id == Id);
            return organization;
        }

        public OrganizationModel GetOrganization(Guid Id)
        {
            var organization = _context.Organizations.Find(Id);
            return organization;
        }

        public bool  DeleteOrganization(Guid Id)
        {
            var organization = _context.Organizations
                .Include(o => o.Users)
                .FirstOrDefault(x => x.Id == Id);

            if (organization == null)
            {
                return false;
            }

            foreach (var user in organization.Users)
            {
                _userService.DeleteUser(user.Id); 
            }

            _context.Organizations.Remove(organization);
            _context.SaveChanges();
            return true;
        }

    }
}
