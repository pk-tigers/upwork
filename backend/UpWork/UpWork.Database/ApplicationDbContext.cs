using Microsoft.EntityFrameworkCore;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserModel>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<UserModel>()
                .HasMany(x => x.SupervisedEmployees)
                .WithOne(x => x.TimeOffSupervisor)
                .HasForeignKey(x => x.TimeOffSupervisorId);
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<OrganizationModel> Organizations { get; set; }
    }
}
