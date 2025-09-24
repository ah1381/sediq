using Example.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Raya.Hrm.Shared.Library.Models.Auth;

namespace Example.Domain.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ActivityFormEntity> ActivityForms { get; set; }
        public DbSet<PhoneNumberEntity> PhoneNumbers { get; set; }
        public DbSet<ProgramEntity> Programs { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<Authentication> Authentications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Authentication>(entity =>
            {
                entity.ToTable("authentication", "auth");
                entity.HasKey(e => e.RowId);

                entity.Property(e => e.RowId)
                    .HasColumnName("RowId")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Username)
                    .HasColumnName("Username")
                    .IsRequired();

                entity.Property(e => e.Password)
                    .HasColumnName("Password")
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .IsRequired(false);

                entity.Property(e => e.OwnerProject)
                    .HasColumnName("OwnerProject")
                    .IsRequired(false);

                entity.Property(e => e.UserType)
                    .HasColumnName("UserType")
                    .IsRequired(false);
            });
        }
    }
}
