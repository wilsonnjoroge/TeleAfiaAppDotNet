using Microsoft.EntityFrameworkCore;
using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;
using TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities;

namespace TeleAfiaAppDotNet.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Practitioner> Practitioners { get; set; }
        public DbSet<CHP> CHPs { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Editor> Editors { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<PractitionerType> PractitionerTypes { get; set; }
        public DbSet<UserType> UserTypes { get; set; } // Rename to Types to avoid collision and confusion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<User>("USER")
                .HasValue<Practitioner>("PRACTITIONER")
                .HasValue<CHP>("CHP")
                .HasValue<Patient>("PATIENT");

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => ur.Id);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.Id);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<PractitionerType>()
                .HasOne(pt => pt.Practitioner)
                .WithMany(p => p.PractitionerTypes)
                .HasForeignKey(pt => pt.PractitionerId);

            modelBuilder.Entity<PractitionerType>()
                .HasOne(pt => pt.Type)
                .WithMany(t => t.PractitionerTypes)
                .HasForeignKey(pt => pt.TypeId);
        }
    }
}
