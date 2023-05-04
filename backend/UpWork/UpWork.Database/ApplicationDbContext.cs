using Microsoft.EntityFrameworkCore;
using UpWork.Common.Enums;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasMany(x => x.SupervisedEmployees)
                .WithOne(x => x.CurrentTimeOffSupervisor)
                .HasForeignKey(x => x.CurrentTimeOffSupervisorId);

            modelBuilder.Entity<AbsenceModel>()
                .HasOne(a => a.User)
                .WithMany(g => g.Absences)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AbsenceModel>()
                .HasOne(a => a.TimeOffSupervisor)
                .WithMany(g => g.AbsencesSupervised)
                .HasForeignKey(a => a.TimeOffSupervisorId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<OrganizationModel> Organizations { get; set; }
        public DbSet<PermissionModel> Permissions { get; set; }
        public DbSet<AbsenceModel> Absences { get; set; }
        public DbSet<AbsenceTypeModel> AbsencesType { get; set; }
    }
}
