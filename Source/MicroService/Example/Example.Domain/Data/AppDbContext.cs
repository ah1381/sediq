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

        public DbSet<ExampleEntity> Examples { get; set; }
        //public DbSet<ErrorModel> ErrorLogs { get; set; }
        //public DbSet<RequestDataModel> RequestLogs { get; set; }
        public DbSet<Authentication> Authentications { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExampleEntity>(entity =>
            {
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

                entity.Property(e => e.RandId).IsRequired(false).HasMaxLength(36);
                entity.Property(e => e.CreatedAt).IsRequired(false);
                entity.Property(e => e.UpdatedAt).IsRequired(false);
                entity.Property(e => e.RevSeq).IsRequired(false);
                entity.Property(e => e.Status).IsRequired(false);
            });

            //modelBuilder.Entity<ErrorModel>(entity =>
            //{
            //    entity.ToTable("error_logs", "error");
            //    entity.HasKey(e => e.RowId);
            //    entity.Property(e => e.RowId).HasColumnName("RowId").ValueGeneratedOnAdd();
            //    entity.Property(e => e.AppName).HasColumnName("app_name").IsRequired();
            //    entity.Property(e => e.Message).HasColumnName("message").IsRequired();
            //    entity.Property(e => e.StackTrace).HasColumnName("stack_trace").IsRequired();
            //    entity.Property(e => e.InnerException).HasColumnName("inner_exception").IsRequired(false);
            //    entity.Property(e => e.Ip).HasColumnName("ip").IsRequired(false);
            //    entity.Property(e => e.RequestId).HasColumnName("request_id").HasColumnType("uuid").IsRequired(false);

            //    // 🔹 New fields mapping for ErrorModel
            //    entity.Property(e => e.ProjectName).HasColumnName("project_name");
            //    entity.Property(e => e.ServiceName).HasColumnName("service_name");
            //    entity.Property(e => e.LogType).HasColumnName("log_type");
            //    entity.Property(e => e.EntityType).HasColumnName("entity_type");
            //    entity.Property(e => e.EntityId).HasColumnName("entity_id");
            //    entity.Property(e => e.EntityRandId).HasColumnName("entity_rand_id");
            //    entity.Property(e => e.Title).HasColumnName("title");
            //    entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            //    entity.Property(e => e.DateTime).HasColumnName("datetime");
            //    entity.Property(e => e.DeviceInfo).HasColumnName("device_info");
            //    entity.Property(e => e.Request).HasColumnName("request");
            //    entity.Property(e => e.Response).HasColumnName("response");
            //    entity.Property(e => e.Status).HasColumnName("status");

            //    entity.Ignore(e => e.Ex);
            //    entity.Ignore(e => e.ConnectionType);
            //});


            //modelBuilder.Entity<RequestDataModel>(entity =>
            //{
            //    entity.ToTable("request_logs", "error");
            //    entity.HasKey(e => e.request_id);

            //    entity.Property(e => e.request_id).HasColumnName("id").HasColumnType("uuid");
            //    entity.Property(e => e.cr).HasColumnName("cr").HasColumnType("timestamp with time zone").IsRequired();
            //    entity.Property(e => e.requesttype).HasColumnName("request_type").IsRequired().HasMaxLength(20);
            //    entity.Property(e => e.Path).HasColumnName("path").IsRequired().HasMaxLength(2048);
            //    entity.Property(e => e.queryString).HasColumnName("query_string").IsRequired(false).HasMaxLength(2048);
            //    entity.Property(e => e.headers).HasColumnName("headers").IsRequired();
            //    entity.Property(e => e.body).HasColumnName("body").IsRequired(false).HasMaxLength(2048);
            //    entity.Property(e => e.clientIp).HasColumnName("client_ip").IsRequired(false).HasMaxLength(45);
            //    entity.Property(e => e.userAgent).HasColumnName("user_agent").IsRequired(false).HasMaxLength(512);
            //    entity.Property(e => e.appName).HasColumnName("app_name").IsRequired().HasMaxLength(256);

            //    // 🔹 New fields mapping
            //    entity.Property(e => e.project_name).HasColumnName("project_name").HasMaxLength(256);
            //    entity.Property(e => e.service_name).HasColumnName("service_name").HasMaxLength(256);
            //    entity.Property(e => e.log_type).HasColumnName("log_type").HasMaxLength(50);
            //    entity.Property(e => e.entity_type).HasColumnName("entity_type").HasMaxLength(100);
            //    entity.Property(e => e.entity_id).HasColumnName("entity_id").HasMaxLength(100);
            //    entity.Property(e => e.entity_rand_id).HasColumnName("entity_rand_id").HasMaxLength(100);
            //    entity.Property(e => e.title).HasColumnName("title").HasMaxLength(256);
            //    entity.Property(e => e.message).HasColumnName("message");
            //    entity.Property(e => e.created_by).HasColumnName("created_by").HasMaxLength(100);
            //    entity.Property(e => e.datetime).HasColumnName("datetime").HasColumnType("timestamp with time zone");
            //    entity.Property(e => e.ip).HasColumnName("ip").HasMaxLength(45);
            //    entity.Property(e => e.device_info).HasColumnName("device_info").HasMaxLength(512);
            //    entity.Property(e => e.request).HasColumnName("request");
            //    entity.Property(e => e.response).HasColumnName("response");
            //    entity.Property(e => e.status).HasColumnName("status").HasMaxLength(50);
            //});

            modelBuilder.Entity<Authentication>(entity =>
            {
                entity.ToTable("authentication", "auth");

                // Use bigserial with Identity always
                entity.HasKey(e => e.RowId);

                entity.Property(e => e.RowId)
                    .HasColumnName("RowId")
                    .UseIdentityAlwaysColumn(); // forces bigserial

                entity.Property(e => e.Username)
                    .HasColumnName("Username")
                    .IsRequired();

                entity.Property(e => e.Password)
                    .HasColumnName("Password")
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .IsRequired(false); // nullable

                entity.Property(e => e.OwnerProject)
                    .HasColumnName("OwnerProject")
                    .IsRequired(false); // nullable

                entity.Property(e => e.UserType)
                    .HasColumnName("UserType")
                    .IsRequired(false); // optional
            });



        }
    }
}