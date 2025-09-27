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
        public DbSet<ImagesEntity> Images { get; set; }
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



                entity.HasData(new Authentication
                {
                    RowId = 1, // میتونی 1 هم بذاری اگر دیتابیس خالیه
                    Username = "ADMIN",
                    Password = "bJKg0LB4QfRPW8dIbQqLyg==",
                    Description = "1",
                    OwnerProject = "AllProjectOwner",
                    UserType = 1
                });
            });

            modelBuilder.Entity<PhoneNumberEntity>()
              .HasOne(p => p.Student)
              .WithMany(s => s.PhoneNumbers)
              .HasForeignKey(p => p.StudentId)
              .OnDelete(DeleteBehavior.Cascade);

            // Student ↔ Images (یک به چند)
            modelBuilder.Entity<ImagesEntity>()
                .HasOne(i => i.Student)
                .WithMany(s => s.Photos)
                .HasForeignKey(i => i.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // ActivityForm ↔ Student (چند به یک)
            modelBuilder.Entity<ActivityFormEntity>()
                .HasOne(a => a.Student)
                .WithMany(s => s.ActivityForms)
                .HasForeignKey(a => a.SelectedStudentId)
                .OnDelete(DeleteBehavior.Restrict); // هر فرم فقط یک دانش‌آموز

            // ActivityForm ↔ Program (چند به یک)
            modelBuilder.Entity<ActivityFormEntity>()
                .HasOne(a => a.SelectedProgram)
                .WithMany(p => p.activityForms)
                .HasForeignKey(a => a.SelectedProgramId)
                .OnDelete(DeleteBehavior.Restrict); // هر فرم فقط یک برنامه

            // تنظیمات کلید اصلی و Identity برای BaseEntity
            modelBuilder.Entity<StudentEntity>()
                .Property(s => s.RowId)
                .UseIdentityAlwaysColumn();

            modelBuilder.Entity<ProgramEntity>()
                .Property(p => p.RowId)
                .UseIdentityAlwaysColumn();

            modelBuilder.Entity<ActivityFormEntity>()
                .Property(a => a.RowId)
                .UseIdentityAlwaysColumn();

            modelBuilder.Entity<PhoneNumberEntity>()
                .Property(p => p.RowId)
                .UseIdentityAlwaysColumn();

            modelBuilder.Entity<ImagesEntity>()
                .Property(i => i.RowId)
                .UseIdentityAlwaysColumn();


            modelBuilder.Entity<StudentEntity>().HasKey(s => s.RowId);
            modelBuilder.Entity<ProgramEntity>().HasKey(p => p.RowId);
            modelBuilder.Entity<ActivityFormEntity>().HasKey(a => a.RowId);
            modelBuilder.Entity<PhoneNumberEntity>().HasKey(p => p.RowId);
            modelBuilder.Entity<ImagesEntity>().HasKey(i => i.RowId);
        }
    }
}
