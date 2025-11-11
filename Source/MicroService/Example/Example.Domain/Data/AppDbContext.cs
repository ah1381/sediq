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
        public DbSet<DurationDateEntity> DurationDateEntitys { get; set; }
        public DbSet<ImagesEntity> Images { get; set; }
        public DbSet<PhoneNumberEntity> PhoneNumbers { get; set; }
        public DbSet<ProgramEntity> Programs { get; set; }
        public DbSet<ScoreFormEntity> ScoreForms { get; set; }
        public DbSet<SediqEntity> Sediqs { get; set; }
        public DbSet<StudentEntity> Students { get; set; }

        public DbSet<Authentication> Authentications { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<PermissionRole> PermissionRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Authentication configuration
            modelBuilder.Entity<Authentication>(entity =>
            {
                entity.ToTable("authentication", "auth");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId).HasColumnName("RowId").UseIdentityAlwaysColumn();
                entity.Property(e => e.Username).HasColumnName("Username").IsRequired();
                entity.Property(e => e.Password).HasColumnName("Password").IsRequired();
                entity.Property(e => e.Description).HasColumnName("Description").IsRequired(false);
                entity.Property(e => e.OwnerProject).HasColumnName("OwnerProject").IsRequired(false);
                entity.Property(e => e.UserType).HasColumnName("UserType").IsRequired(false);

                entity.HasData(new Authentication
                {
                    RowId = 1,
                    Username = "ADMIN",
                    Password = "bJKg0LB4QfRPW8dIbQqLyg==",
                    Description = "1",
                    OwnerProject = "AllProjectOwner",
                    UserType = 1
                });
            });

            // Primary Keys
            modelBuilder.Entity<StudentEntity>().HasKey(s => s.RowId);
            modelBuilder.Entity<ProgramEntity>().HasKey(p => p.RowId);
            modelBuilder.Entity<ActivityFormEntity>().HasKey(a => a.RowId);
            modelBuilder.Entity<PhoneNumberEntity>().HasKey(p => p.RowId);
            modelBuilder.Entity<ImagesEntity>().HasKey(i => i.RowId);
            modelBuilder.Entity<DurationDateEntity>().HasKey(d => d.RowId);
            modelBuilder.Entity<ScoreFormEntity>().HasKey(s => s.RowId);
            modelBuilder.Entity<SediqEntity>().HasKey(s => s.RowId);

            // Unique Constraints
            modelBuilder.Entity<SediqEntity>().HasIndex(s => s.SediqCode).IsUnique();
            modelBuilder.Entity<SediqEntity>().Property(s => s.SediqCode).IsRequired();
            modelBuilder.Entity<Role>().HasKey(r => r.RowId);
            modelBuilder.Entity<UserRole>().HasKey(ur => ur.RowId);
            modelBuilder.Entity<Permission>().HasKey(p => p.RowId);
            modelBuilder.Entity<PermissionRole>().HasKey(pr => pr.RowId);

            // Identity Columns
            modelBuilder.Entity<StudentEntity>().Property(s => s.RowId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<ProgramEntity>().Property(p => p.RowId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<ActivityFormEntity>().Property(a => a.RowId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<PhoneNumberEntity>().Property(p => p.RowId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<ImagesEntity>().Property(i => i.RowId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<DurationDateEntity>().Property(d => d.RowId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<ScoreFormEntity>().Property(s => s.RowId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<SediqEntity>().Property(s => s.RowId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<Role>().Property(r => r.RowId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<UserRole>().Property(ur => ur.RowId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<Permission>().Property(p => p.RowId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<PermissionRole>().Property(pr => pr.RowId).UseIdentityAlwaysColumn();

            // RandId nullable configuration
            modelBuilder.Entity<SediqEntity>().Property(s => s.RandId).IsRequired(false);
            modelBuilder.Entity<StudentEntity>().Property(s => s.RandId).IsRequired(false);
            modelBuilder.Entity<ProgramEntity>().Property(p => p.RandId).IsRequired(false);
            modelBuilder.Entity<ActivityFormEntity>().Property(a => a.RandId).IsRequired(false);
            modelBuilder.Entity<ScoreFormEntity>().Property(s => s.RandId).IsRequired(false);
            modelBuilder.Entity<DurationDateEntity>().Property(d => d.RandId).IsRequired(false);
            modelBuilder.Entity<PhoneNumberEntity>().Property(p => p.RandId).IsRequired(false);
            modelBuilder.Entity<ImagesEntity>().Property(i => i.RandId).IsRequired(false);

            // Student ↔ PhoneNumber (یک به چند)
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

            // Student ↔ Sediq (چند به یک) - based on SediqCode
            modelBuilder.Entity<StudentEntity>()
                .HasOne(s => s.sediq)
                .WithMany(sd => sd.Students)
                .HasForeignKey(s => s.sediqRowId)
                .HasPrincipalKey(sd => sd.SediqCode)
                .OnDelete(DeleteBehavior.SetNull);

            // ActivityForm ↔ Student (چند به یک)
            modelBuilder.Entity<ActivityFormEntity>()
                .HasOne(a => a.Student)
                .WithMany(s => s.ActivityForms)
                .HasForeignKey(a => a.SelectedStudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // ActivityForm ↔ Program (چند به یک)
            modelBuilder.Entity<ActivityFormEntity>()
                .HasOne(a => a.SelectedProgram)
                .WithMany(p => p.activityForms)
                .HasForeignKey(a => a.SelectedProgramId)
                .OnDelete(DeleteBehavior.Restrict);

            // ScoreForm ↔ Program (چند به یک)
            modelBuilder.Entity<ScoreFormEntity>()
                .HasOne(s => s.SelectedProgram)
                .WithMany(p => p.ScoreForms)
                .HasForeignKey(s => s.SelectedProgramId)
                .OnDelete(DeleteBehavior.Restrict);

            // ScoreForm ↔ Student (چند به یک)
            modelBuilder.Entity<ScoreFormEntity>()
                .HasOne(s => s.SelectedStudent)
                .WithMany(st => st.ScoreForms)
                .HasForeignKey(s => s.SelectedStudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // ScoreForm ↔ DurationDate (چند به یک)
            modelBuilder.Entity<ScoreFormEntity>()
                .HasOne(s => s.ActivityDur)
                .WithMany(d => d.ScoreForm)
                .HasForeignKey(s => s.ActivityDurId)
                .OnDelete(DeleteBehavior.Restrict);

            // Precision Settings
            modelBuilder.Entity<ScoreFormEntity>()
                .Property(s => s.Score)
                .HasPrecision(5, 2);
        }
    }
}