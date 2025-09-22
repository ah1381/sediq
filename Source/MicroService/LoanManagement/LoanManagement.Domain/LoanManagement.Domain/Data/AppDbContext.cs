using LoanManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Domain.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<PersonnelEntity> Personnel { get; set; }
        public DbSet<FundEntity> Funds { get; set; }
        public DbSet<FundHierarchyEntity> FundHierarchies { get; set; }
        public DbSet<FundRegionEntity> FundRegions { get; set; }
        public DbSet<FundMemberEntity> FundMembers { get; set; }
        public DbSet<FundExecutiveEntity> FundExecutives { get; set; }
        public DbSet<FundInspectorEntity> FundInspectors { get; set; }
        public DbSet<FundSettingEntity> FundSettings { get; set; }
        public DbSet<FundTransactionEntity> FundTransactions { get; set; }
        public DbSet<LoanAdjustmentRequestEntity> LoanAdjustmentRequests { get; set; }
        public DbSet<LoanRequestEntity> LoanRequests { get; set; }
        public DbSet<LoanRequestLogEntity> LoanRequestLogs { get; set; }
        public DbSet<LoanTypeEntity> LoanTypes { get; set; }
        public DbSet<LoanRepaymentEntity> LoanRepayments { get; set; }
        public DbSet<LoanCertificateEntity> LoanCertificates { get; set; }
        public DbSet<LoanGuarantorEntity> LoanGuarantors { get; set; }
        public DbSet<FundElectionEntity> FundElections { get; set; }
        public DbSet<FundTransferEntity> FundTransfers { get; set; }
        public DbSet<FundInsuranceEntity> FundInsurances { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<FundReportEntity> FundReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PersonnelEntity
            modelBuilder.Entity<PersonnelEntity>(entity =>
            {
                entity.ToTable("Personnel", "Entity");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.NationalCode).HasMaxLength(20).IsRequired();
                entity.Property(e => e.EmploymentCode).HasMaxLength(50).IsRequired();
                entity.Property(e => e.BirthDate).HasColumnType("timestamp with time zone").IsRequired(false);
                entity.Property(e => e.Phone).HasMaxLength(20).IsRequired(false);
                entity.Property(e => e.Mobile).HasMaxLength(20).IsRequired(false);
                entity.Property(e => e.Address).IsRequired(false);
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.FatherName).HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.Department).HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.Province).HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.EmploymentType).HasMaxLength(50).IsRequired(false);
            });

            // FundEntity
            modelBuilder.Entity<FundEntity>(entity =>
            {
                entity.ToTable("Fund", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
                entity.Property(e => e.FundType).HasMaxLength(50).IsRequired();
                entity.Property(e => e.CalculationType).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Region).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Province).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Level).HasMaxLength(50).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
            });

            // FundHierarchyEntity
            modelBuilder.Entity<FundHierarchyEntity>(entity =>
            {
                entity.ToTable("FundHierarchy", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.FundID).IsRequired(false); // Match long?
                entity.Property(e => e.ParentFundID).IsRequired(false); // Match long?
                entity.Property(e => e.Description).IsRequired(false);
                entity.HasOne(e => e.Fund).WithMany().HasForeignKey(e => e.FundID).IsRequired();
                entity.HasOne(e => e.ParentFund).WithMany().HasForeignKey(e => e.ParentFundID).IsRequired(false);
            });

            // FundRegionEntity
            modelBuilder.Entity<FundRegionEntity>(entity =>
            {
                entity.ToTable("FundRegion", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.FundID).IsRequired(); // Required, non-nullable
                entity.Property(e => e.RegionName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ActiveMembers).IsRequired();
                entity.Property(e => e.RetiredMembers).IsRequired();
                entity.Property(e => e.BankName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.BranchCode).HasMaxLength(50).IsRequired();
                entity.Property(e => e.OrgCode).HasMaxLength(50).IsRequired();
                entity.Property(e => e.InstallmentDeductionCodes).HasColumnType("text[]").IsRequired();
                entity.Property(e => e.ShareDeductionCodes).HasColumnType("text[]").IsRequired();
                entity.HasOne(e => e.Fund).WithMany().HasForeignKey(e => e.FundID).IsRequired();
            });

            // FundMemberEntity
            modelBuilder.Entity<FundMemberEntity>(entity =>
            {
                entity.ToTable("FundMember", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.PersonnelID).IsRequired(false);
                entity.Property(e => e.FundID).IsRequired(false);
                entity.Property(e => e.MembershipType).HasMaxLength(50).IsRequired();
                entity.Property(e => e.MembershipNumber).HasMaxLength(50).IsRequired();
                entity.Property(e => e.StartDate).HasColumnType("timestamp with time zone").IsRequired(false);
                entity.Property(e => e.EndDate).HasColumnType("timestamp with time zone").IsRequired(false);
                entity.Property(e => e.Shares).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.ShareDeductionCodes).HasColumnType("text[]").IsRequired();
                entity.Property(e => e.BankAccount).HasMaxLength(50).IsRequired();
                entity.Property(e => e.EmploymentType).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Position).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.Phone).HasMaxLength(20).IsRequired(false);
                entity.Property(e => e.StatusDesc).HasMaxLength(50).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.Property(e => e.TotalSalary).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.NetPayment).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.Province).HasMaxLength(100).IsRequired();
                entity.Property(e => e.WaitingMonths).IsRequired();
                entity.HasOne(e => e.Personnel).WithMany().HasForeignKey(e => e.PersonnelID).IsRequired();
                entity.HasOne(e => e.Fund).WithMany().HasForeignKey(e => e.FundID).IsRequired();
            });

            // FundExecutiveEntity
            modelBuilder.Entity<FundExecutiveEntity>(entity =>
            {
                entity.ToTable("FundExecutive", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.FundID).IsRequired(); // Required, non-nullable
                entity.Property(e => e.PersonnelID).IsRequired(); // Required, assuming PersonnelID is non-nullable
                entity.Property(e => e.Role).HasMaxLength(50).IsRequired();
                entity.Property(e => e.StartDate).HasColumnType("timestamp with time zone").IsRequired(false);
                entity.Property(e => e.EndDate).HasColumnType("timestamp with time zone").IsRequired(false);
                entity.Property(e => e.HasSignPermission).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.Property(e => e.OrderNumber).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Elected).IsRequired();
                entity.HasOne(e => e.Fund).WithMany().HasForeignKey(e => e.FundID).IsRequired();
                entity.HasOne(e => e.Personnel).WithMany().HasForeignKey(e => e.PersonnelID).IsRequired();
            });

            // FundInspectorEntity
            modelBuilder.Entity<FundInspectorEntity>(entity =>
            {
                entity.ToTable("FundInspector", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.FundID).IsRequired(); // Required, non-nullable
                entity.Property(e => e.PersonnelID).IsRequired(); // Required, non-nullable
                entity.Property(e => e.StartDate).HasColumnType("timestamp with time zone").IsRequired(false);
                entity.Property(e => e.EndDate).HasColumnType("timestamp with time zone").IsRequired(false);
                entity.Property(e => e.ReportFrequency).HasMaxLength(50).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.HasOne(e => e.Fund).WithMany().HasForeignKey(e => e.FundID).IsRequired();
                entity.HasOne(e => e.Personnel).WithMany().HasForeignKey(e => e.PersonnelID).IsRequired();
            });

            // FundSettingEntity
            modelBuilder.Entity<FundSettingEntity>(entity =>
            {
                entity.ToTable("FundSetting", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.FundID).IsRequired(); // Required, non-nullable
                entity.Property(e => e.MaxLoanAmount).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.LoanMultiplier).HasPrecision(5, 2).IsRequired();
                entity.Property(e => e.CommissionPercent).HasPrecision(5, 2).IsRequired();
                entity.Property(e => e.MinShareAmount).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.MinMembershipMonths).IsRequired();
                entity.Property(e => e.SingleLoanPerMember).IsRequired();
                entity.Property(e => e.MultiPaymentAllowed).IsRequired();
                entity.Property(e => e.MaxInstallmentChange).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.RepaymentMonths).HasDefaultValue((short)36).IsRequired();
                entity.Property(e => e.InsurancePercent).HasPrecision(5, 2).IsRequired();
                entity.HasOne(e => e.Fund).WithMany().HasForeignKey(e => e.FundID).IsRequired();
            });

            // FundTransactionEntity
            modelBuilder.Entity<FundTransactionEntity>(entity =>
            {
                entity.ToTable("FundTransaction", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.FundID).IsRequired(); // Required, non-nullable
                entity.Property(e => e.MemberID).IsRequired(); // Required, non-nullable
                entity.Property(e => e.TransactionDate).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.TransactionType).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Amount).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.Balance).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.Description).IsRequired(false);
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.Property(e => e.DebitAmount).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.CreditAmount).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.Commission).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.Insurance).HasPrecision(18, 2).IsRequired();
                entity.HasOne(e => e.Fund).WithMany().HasForeignKey(e => e.FundID).IsRequired();
                entity.HasOne(e => e.Member).WithMany().HasForeignKey(e => e.MemberID).IsRequired();
            });

            // LoanAdjustmentRequestEntity
            modelBuilder.Entity<LoanAdjustmentRequestEntity>(entity =>
            {
                entity.ToTable("LoanAdjustmentRequest", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.MemberID).IsRequired(); // Required, non-nullable
                entity.Property(e => e.RequestType).HasMaxLength(50).IsRequired();
                entity.Property(e => e.OldValue).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.NewValue).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.MaxAllowed).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.Description).IsRequired(false);
                entity.Property(e => e.RequestDate).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.StatusDesc).HasMaxLength(50).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Online).IsRequired();
                entity.HasOne(e => e.Member).WithMany().HasForeignKey(e => e.MemberID).IsRequired();
            });

            // LoanRequestEntity
            modelBuilder.Entity<LoanRequestEntity>(entity =>
            {
                entity.ToTable("LoanRequest", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.PersonnelID).IsRequired(); // Match long?
                entity.Property(e => e.LoanTypeID).IsRequired(); // Match long?
                entity.Property(e => e.FundID).IsRequired(); // Match long?
                entity.Property(e => e.RequestDate).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.AmountRequested).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.MaxLoanAmount).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.WaitingMonths).IsRequired();
                entity.Property(e => e.StatusDesc).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Installments).IsRequired();
                entity.Property(e => e.MonthlyInstallment).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.BankAccount).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Address).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Phone).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Mobile).HasMaxLength(20).IsRequired();
                entity.Property(e => e.RequiresGuarantor).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Province).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Region).HasMaxLength(100).IsRequired();
                entity.Property(e => e.EmploymentType).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Department).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Online).IsRequired();
                entity.HasOne(e => e.Personnel).WithMany().HasForeignKey(e => e.PersonnelID).IsRequired();
                entity.HasOne(e => e.LoanType).WithMany().HasForeignKey(e => e.LoanTypeID).IsRequired();
                entity.HasOne(e => e.Fund).WithMany().HasForeignKey(e => e.FundID).IsRequired();
            });

            // LoanRequestLogEntity
            modelBuilder.Entity<LoanRequestLogEntity>(entity =>
            {
                entity.ToTable("LoanRequestLog", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.LoanRequestID).IsRequired();
                entity.Property(e => e.Action).HasMaxLength(50).IsRequired();
                entity.Property(e => e.ActionBy).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ActionDate).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.Note).IsRequired(false);
                entity.HasOne(e => e.LoanRequest).WithMany().HasForeignKey(e => e.LoanRequestID).IsRequired();
            });

            // LoanTypeEntity
            modelBuilder.Entity<LoanTypeEntity>(entity =>
            {
                entity.ToTable("LoanType", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.FundID).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).IsRequired(false);
                entity.Property(e => e.MaxAmount).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.MinInstallments).IsRequired();
                entity.Property(e => e.MaxInstallments).IsRequired();
                entity.Property(e => e.StartDate).HasColumnType("timestamp with time zone").IsRequired(false);
                entity.Property(e => e.EndDate).HasColumnType("timestamp with time zone").IsRequired(false);
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.HasOne(e => e.Fund).WithMany().HasForeignKey(e => e.FundID).IsRequired();
            });

            // LoanRepaymentEntity
            modelBuilder.Entity<LoanRepaymentEntity>(entity =>
            {
                entity.ToTable("LoanRepayment", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.LoanRequestID).IsRequired();
                entity.Property(e => e.PaymentDate).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.Amount).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.Balance).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.ReceiptNumber).HasMaxLength(50).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.Property(e => e.PaymentMethod).HasMaxLength(50).IsRequired();
                entity.Property(e => e.PaymentGatewayRef).HasMaxLength(100).IsRequired();
                entity.HasOne(e => e.LoanRequest).WithMany().HasForeignKey(e => e.LoanRequestID).IsRequired();
            });

            // LoanCertificateEntity
            modelBuilder.Entity<LoanCertificateEntity>(entity =>
            {
                entity.ToTable("LoanCertificate", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.LoanRequestID).IsRequired();
                entity.Property(e => e.CertificateNumber).HasMaxLength(50).IsRequired();
                entity.Property(e => e.IssueDate).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.Description).IsRequired(false);
                entity.HasOne(e => e.LoanRequest).WithMany().HasForeignKey(e => e.LoanRequestID).IsRequired();
            });

            // LoanGuarantorEntity
            modelBuilder.Entity<LoanGuarantorEntity>(entity =>
            {
                entity.ToTable("LoanGuarantor", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.LoanRequestID).IsRequired();
                entity.Property(e => e.PersonnelID).IsRequired();
                entity.Property(e => e.GuarantorCode).HasMaxLength(50).IsRequired();
                entity.Property(e => e.GuaranteeDate).HasColumnType("timestamp with time zone").IsRequired(false);
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.HasOne(e => e.LoanRequest).WithMany().HasForeignKey(e => e.LoanRequestID).IsRequired();
                entity.HasOne(e => e.Personnel).WithMany().HasForeignKey(e => e.PersonnelID).IsRequired();
            });

            // FundElectionEntity
            modelBuilder.Entity<FundElectionEntity>(entity =>
            {
                entity.ToTable("FundElection", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.FundID).IsRequired();
                entity.Property(e => e.ElectionDate).HasColumnType("timestamp with time zone").IsRequired(false);
                entity.Property(e => e.CandidateID).IsRequired();
                entity.Property(e => e.Votes).IsRequired();
                entity.Property(e => e.Position).HasMaxLength(50).IsRequired();
                entity.Property(e => e.FundStatus).HasMaxLength(50).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.HasOne(e => e.Fund).WithMany().HasForeignKey(e => e.FundID).IsRequired();
                entity.HasOne(e => e.Candidate).WithMany().HasForeignKey(e => e.CandidateID).IsRequired();
            });

            // FundTransferEntity
            modelBuilder.Entity<FundTransferEntity>(entity =>
            {
                entity.ToTable("FundTransfer", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.MemberID).IsRequired();
                entity.Property(e => e.FromFundID).IsRequired();
                entity.Property(e => e.ToFundID).IsRequired();
                entity.Property(e => e.TransferDate).HasColumnType("timestamp with time zone").IsRequired(false);
                entity.Property(e => e.TransferType).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Amount).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.Description).IsRequired(false);
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.HasOne(e => e.Member).WithMany().HasForeignKey(e => e.MemberID).IsRequired();
                entity.HasOne(e => e.FromFund).WithMany().HasForeignKey(e => e.FromFundID).IsRequired();
                entity.HasOne(e => e.ToFund).WithMany().HasForeignKey(e => e.ToFundID).IsRequired();
            });

            // FundInsuranceEntity
            modelBuilder.Entity<FundInsuranceEntity>(entity =>
            {
                entity.ToTable("FundInsurance", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.FundID).IsRequired();
                entity.Property(e => e.LoanRequestID).IsRequired(false);
                entity.Property(e => e.InsuranceAmount).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.InsuranceDate).HasColumnType("timestamp with time zone").IsRequired(false);
                entity.Property(e => e.Provider).HasMaxLength(100).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.HasOne(e => e.Fund).WithMany().HasForeignKey(e => e.FundID).IsRequired();
                entity.HasOne(e => e.LoanRequest).WithMany().HasForeignKey(e => e.LoanRequestID).IsRequired(false);
            });

            // NotificationEntity
            modelBuilder.Entity<NotificationEntity>(entity =>
            {
                entity.ToTable("Notification", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.PersonnelID).IsRequired();
                entity.Property(e => e.NotificationType).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Message).IsRequired();
                entity.Property(e => e.NotificationDate).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.Channel).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Read).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.HasOne(e => e.Personnel).WithMany().HasForeignKey(e => e.PersonnelID).IsRequired();
            });

            // FundReportEntity
            modelBuilder.Entity<FundReportEntity>(entity =>
            {
                entity.ToTable("FundReport", "LON");
                entity.HasKey(e => e.RowId);
                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasColumnType("serial")
                    .ValueGeneratedOnAdd()
                    .UseSerialColumn();
                entity.Property(e => e.RandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("now()").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone").IsRequired();
                entity.Property(e => e.RevSeq).HasDefaultValue((short)1).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.FundID).IsRequired();
                entity.Property(e => e.ReportYear).IsRequired();
                entity.Property(e => e.ReportType).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Balance).HasPrecision(18, 2).IsRequired();
                entity.Property(e => e.Description).IsRequired(false);
                entity.Property(e => e.CreatedBy).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Approved).IsRequired();
                entity.HasOne(e => e.Fund).WithMany().HasForeignKey(e => e.FundID).IsRequired();
            });
        }
    }
}
