using Microsoft.EntityFrameworkCore;
using UpWork.Common.DTO;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Infrastucture.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ApplicationDbContext _context;

        public OrganizationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public OrganizationModel CreateOrganization(CreateOrganizationDTO organizationDTO)
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

        public void AddUserToOrganization(UserOrganizationDto userOrganizationDto)
        {
            var organization = _context.Organizations
                .Include(o => o.Users)
                .FirstOrDefault(o => o.Id == userOrganizationDto.OrganizationId);

            var user = _context.Users.Find(userOrganizationDto.UserId);

            if (organization != null)
            {
                organization.Users.Add(user);
                _context.SaveChanges();
            }

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

        public void  DeleteOrganization(Guid Id)
        {
            var organization = _context.Organizations.Find(Id);
            _context.Organizations.Remove(organization);
            _context.SaveChanges();
        }

        public void DeleteUserFromOrganization(UserOrganizationDto userOrganizationDto)
        {
            var organization = _context.Organizations
                .Include(o => o.Users)
                .FirstOrDefault(o => o.Id == userOrganizationDto.OrganizationId);

            var user = _context.Users.Find(userOrganizationDto.UserId);

            if (organization != null)
            {
                organization.Users.Remove(user);
                _context.SaveChanges();
            }
        }



    }
}
