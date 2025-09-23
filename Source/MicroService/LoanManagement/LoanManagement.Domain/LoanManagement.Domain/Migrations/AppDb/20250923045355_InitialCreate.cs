using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LoanManagement.Domain.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.EnsureSchema(
                name: "LON");

            migrationBuilder.EnsureSchema(
                name: "Entity");

            migrationBuilder.CreateTable(
                name: "authentication",
                schema: "auth",
                columns: table => new
                {
                    RowId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    OwnerProject = table.Column<string>(type: "text", nullable: true),
                    UserType = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authentication", x => x.RowId);
                });

            migrationBuilder.CreateTable(
                name: "Fund",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FundType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CalculationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Province = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Level = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fund", x => x.row_id);
                });

            migrationBuilder.CreateTable(
                name: "Personnel",
                schema: "Entity",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NationalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    EmploymentCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Mobile = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FatherName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Department = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Province = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EmploymentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel", x => x.row_id);
                });

            migrationBuilder.CreateTable(
                name: "FundHierarchy",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FundID = table.Column<long>(type: "serial", nullable: true),
                    ParentFundID = table.Column<long>(type: "serial", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundHierarchy", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_FundHierarchy_Fund_FundID",
                        column: x => x.FundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundHierarchy_Fund_ParentFundID",
                        column: x => x.ParentFundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id");
                });

            migrationBuilder.CreateTable(
                name: "FundRegion",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FundID = table.Column<long>(type: "serial", nullable: false),
                    RegionName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ActiveMembers = table.Column<int>(type: "integer", nullable: false),
                    RetiredMembers = table.Column<int>(type: "integer", nullable: false),
                    BankName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BranchCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OrgCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InstallmentDeductionCodes = table.Column<List<string>>(type: "text[]", nullable: false),
                    ShareDeductionCodes = table.Column<List<string>>(type: "text[]", nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundRegion", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_FundRegion_Fund_FundID",
                        column: x => x.FundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundReport",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FundID = table.Column<long>(type: "serial", nullable: false),
                    ReportYear = table.Column<int>(type: "integer", nullable: false),
                    ReportType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Approved = table.Column<bool>(type: "boolean", nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundReport", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_FundReport_Fund_FundID",
                        column: x => x.FundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundSetting",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FundID = table.Column<long>(type: "serial", nullable: false),
                    MaxLoanAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    LoanMultiplier = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    CommissionPercent = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    MinShareAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    MinMembershipMonths = table.Column<int>(type: "integer", nullable: false),
                    SingleLoanPerMember = table.Column<bool>(type: "boolean", nullable: false),
                    MultiPaymentAllowed = table.Column<bool>(type: "boolean", nullable: false),
                    MaxInstallmentChange = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    RepaymentMonths = table.Column<int>(type: "integer", nullable: false, defaultValue: 36),
                    InsurancePercent = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundSetting", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_FundSetting_Fund_FundID",
                        column: x => x.FundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanType",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FundID = table.Column<long>(type: "serial", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MaxAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    MinInstallments = table.Column<int>(type: "integer", nullable: false),
                    MaxInstallments = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanType", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_LoanType_Fund_FundID",
                        column: x => x.FundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundElection",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FundID = table.Column<long>(type: "serial", nullable: false),
                    ElectionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CandidateID = table.Column<long>(type: "serial", nullable: false),
                    Votes = table.Column<int>(type: "integer", nullable: false),
                    Position = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FundStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundElection", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_FundElection_Fund_FundID",
                        column: x => x.FundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundElection_Personnel_CandidateID",
                        column: x => x.CandidateID,
                        principalSchema: "Entity",
                        principalTable: "Personnel",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundExecutive",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FundID = table.Column<long>(type: "serial", nullable: false),
                    PersonnelID = table.Column<long>(type: "serial", nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    HasSignPermission = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OrderNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Elected = table.Column<bool>(type: "boolean", nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundExecutive", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_FundExecutive_Fund_FundID",
                        column: x => x.FundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundExecutive_Personnel_PersonnelID",
                        column: x => x.PersonnelID,
                        principalSchema: "Entity",
                        principalTable: "Personnel",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundInspector",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FundID = table.Column<long>(type: "serial", nullable: false),
                    PersonnelID = table.Column<long>(type: "serial", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReportFrequency = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundInspector", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_FundInspector_Fund_FundID",
                        column: x => x.FundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundInspector_Personnel_PersonnelID",
                        column: x => x.PersonnelID,
                        principalSchema: "Entity",
                        principalTable: "Personnel",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundMember",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PersonnelID = table.Column<long>(type: "serial", nullable: true),
                    FundID = table.Column<long>(type: "serial", nullable: true),
                    MembershipType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MembershipNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Shares = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ShareDeductionCodes = table.Column<List<string>>(type: "text[]", nullable: false),
                    BankAccount = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EmploymentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Position = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    StatusDesc = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TotalSalary = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    NetPayment = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Province = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    WaitingMonths = table.Column<int>(type: "integer", nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundMember", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_FundMember_Fund_FundID",
                        column: x => x.FundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundMember_Personnel_PersonnelID",
                        column: x => x.PersonnelID,
                        principalSchema: "Entity",
                        principalTable: "Personnel",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PersonnelID = table.Column<long>(type: "serial", nullable: false),
                    NotificationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    NotificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Channel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Read = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NotificationStatus = table.Column<short>(type: "smallint", nullable: true),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_Notification_Personnel_PersonnelID",
                        column: x => x.PersonnelID,
                        principalSchema: "Entity",
                        principalTable: "Personnel",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanRequest",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PersonnelID = table.Column<long>(type: "serial", nullable: false),
                    LoanTypeID = table.Column<long>(type: "serial", nullable: false),
                    FundID = table.Column<long>(type: "serial", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AmountRequested = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    MaxLoanAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    WaitingMonths = table.Column<int>(type: "integer", nullable: false),
                    StatusDesc = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Installments = table.Column<int>(type: "integer", nullable: false),
                    MonthlyInstallment = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    BankAccount = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Mobile = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RequiresGuarantor = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Province = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EmploymentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Department = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Online = table.Column<bool>(type: "boolean", nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRequest", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_LoanRequest_Fund_FundID",
                        column: x => x.FundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanRequest_LoanType_LoanTypeID",
                        column: x => x.LoanTypeID,
                        principalSchema: "LON",
                        principalTable: "LoanType",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanRequest_Personnel_PersonnelID",
                        column: x => x.PersonnelID,
                        principalSchema: "Entity",
                        principalTable: "Personnel",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundTransaction",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FundID = table.Column<long>(type: "serial", nullable: false),
                    MemberID = table.Column<long>(type: "serial", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransactionType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DebitAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreditAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Commission = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Insurance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundTransaction", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_FundTransaction_FundMember_MemberID",
                        column: x => x.MemberID,
                        principalSchema: "LON",
                        principalTable: "FundMember",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundTransaction_Fund_FundID",
                        column: x => x.FundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundTransfer",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MemberID = table.Column<long>(type: "serial", nullable: false),
                    FromFundID = table.Column<long>(type: "serial", nullable: false),
                    ToFundID = table.Column<long>(type: "serial", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TransferType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundTransfer", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_FundTransfer_FundMember_MemberID",
                        column: x => x.MemberID,
                        principalSchema: "LON",
                        principalTable: "FundMember",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundTransfer_Fund_FromFundID",
                        column: x => x.FromFundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundTransfer_Fund_ToFundID",
                        column: x => x.ToFundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanAdjustmentRequest",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MemberID = table.Column<long>(type: "serial", nullable: false),
                    RequestType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OldValue = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    NewValue = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    MaxAllowed = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    StatusDesc = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Online = table.Column<bool>(type: "boolean", nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanAdjustmentRequest", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_LoanAdjustmentRequest_FundMember_MemberID",
                        column: x => x.MemberID,
                        principalSchema: "LON",
                        principalTable: "FundMember",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundInsurance",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FundID = table.Column<long>(type: "serial", nullable: false),
                    LoanRequestID = table.Column<long>(type: "serial", nullable: true),
                    InsuranceAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    InsuranceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Provider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundInsurance", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_FundInsurance_Fund_FundID",
                        column: x => x.FundID,
                        principalSchema: "LON",
                        principalTable: "Fund",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundInsurance_LoanRequest_LoanRequestID",
                        column: x => x.LoanRequestID,
                        principalSchema: "LON",
                        principalTable: "LoanRequest",
                        principalColumn: "row_id");
                });

            migrationBuilder.CreateTable(
                name: "LoanCertificate",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LoanRequestID = table.Column<long>(type: "serial", nullable: false),
                    CertificateNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanCertificate", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_LoanCertificate_LoanRequest_LoanRequestID",
                        column: x => x.LoanRequestID,
                        principalSchema: "LON",
                        principalTable: "LoanRequest",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanGuarantor",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LoanRequestID = table.Column<long>(type: "serial", nullable: false),
                    PersonnelID = table.Column<long>(type: "serial", nullable: false),
                    GuarantorCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    GuaranteeDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LoanGuarantorStatus = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanGuarantor", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_LoanGuarantor_LoanRequest_LoanRequestID",
                        column: x => x.LoanRequestID,
                        principalSchema: "LON",
                        principalTable: "LoanRequest",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanGuarantor_Personnel_PersonnelID",
                        column: x => x.PersonnelID,
                        principalSchema: "Entity",
                        principalTable: "Personnel",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanRepayment",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LoanRequestID = table.Column<long>(type: "serial", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ReceiptNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PaymentMethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PaymentGatewayRef = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRepayment", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_LoanRepayment_LoanRequest_LoanRequestID",
                        column: x => x.LoanRequestID,
                        principalSchema: "LON",
                        principalTable: "LoanRequest",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanRequestLog",
                schema: "LON",
                columns: table => new
                {
                    row_id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LoanRequestID = table.Column<long>(type: "serial", nullable: false),
                    Action = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ActionBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ActionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    RandId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevSeq = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRequestLog", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_LoanRequestLog_LoanRequest_LoanRequestID",
                        column: x => x.LoanRequestID,
                        principalSchema: "LON",
                        principalTable: "LoanRequest",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FundElection_CandidateID",
                schema: "LON",
                table: "FundElection",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "IX_FundElection_FundID",
                schema: "LON",
                table: "FundElection",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_FundExecutive_FundID",
                schema: "LON",
                table: "FundExecutive",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_FundExecutive_PersonnelID",
                schema: "LON",
                table: "FundExecutive",
                column: "PersonnelID");

            migrationBuilder.CreateIndex(
                name: "IX_FundHierarchy_FundID",
                schema: "LON",
                table: "FundHierarchy",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_FundHierarchy_ParentFundID",
                schema: "LON",
                table: "FundHierarchy",
                column: "ParentFundID");

            migrationBuilder.CreateIndex(
                name: "IX_FundInspector_FundID",
                schema: "LON",
                table: "FundInspector",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_FundInspector_PersonnelID",
                schema: "LON",
                table: "FundInspector",
                column: "PersonnelID");

            migrationBuilder.CreateIndex(
                name: "IX_FundInsurance_FundID",
                schema: "LON",
                table: "FundInsurance",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_FundInsurance_LoanRequestID",
                schema: "LON",
                table: "FundInsurance",
                column: "LoanRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_FundMember_FundID",
                schema: "LON",
                table: "FundMember",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_FundMember_PersonnelID",
                schema: "LON",
                table: "FundMember",
                column: "PersonnelID");

            migrationBuilder.CreateIndex(
                name: "IX_FundRegion_FundID",
                schema: "LON",
                table: "FundRegion",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_FundReport_FundID",
                schema: "LON",
                table: "FundReport",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_FundSetting_FundID",
                schema: "LON",
                table: "FundSetting",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransaction_FundID",
                schema: "LON",
                table: "FundTransaction",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransaction_MemberID",
                schema: "LON",
                table: "FundTransaction",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfer_FromFundID",
                schema: "LON",
                table: "FundTransfer",
                column: "FromFundID");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfer_MemberID",
                schema: "LON",
                table: "FundTransfer",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfer_ToFundID",
                schema: "LON",
                table: "FundTransfer",
                column: "ToFundID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAdjustmentRequest_MemberID",
                schema: "LON",
                table: "LoanAdjustmentRequest",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanCertificate_LoanRequestID",
                schema: "LON",
                table: "LoanCertificate",
                column: "LoanRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanGuarantor_LoanRequestID",
                schema: "LON",
                table: "LoanGuarantor",
                column: "LoanRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanGuarantor_PersonnelID",
                schema: "LON",
                table: "LoanGuarantor",
                column: "PersonnelID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepayment_LoanRequestID",
                schema: "LON",
                table: "LoanRepayment",
                column: "LoanRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRequest_FundID",
                schema: "LON",
                table: "LoanRequest",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRequest_LoanTypeID",
                schema: "LON",
                table: "LoanRequest",
                column: "LoanTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRequest_PersonnelID",
                schema: "LON",
                table: "LoanRequest",
                column: "PersonnelID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRequestLog_LoanRequestID",
                schema: "LON",
                table: "LoanRequestLog",
                column: "LoanRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanType_FundID",
                schema: "LON",
                table: "LoanType",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_PersonnelID",
                schema: "LON",
                table: "Notification",
                column: "PersonnelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "authentication",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "FundElection",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "FundExecutive",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "FundHierarchy",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "FundInspector",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "FundInsurance",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "FundRegion",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "FundReport",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "FundSetting",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "FundTransaction",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "FundTransfer",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "LoanAdjustmentRequest",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "LoanCertificate",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "LoanGuarantor",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "LoanRepayment",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "LoanRequestLog",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "Notification",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "FundMember",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "LoanRequest",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "LoanType",
                schema: "LON");

            migrationBuilder.DropTable(
                name: "Personnel",
                schema: "Entity");

            migrationBuilder.DropTable(
                name: "Fund",
                schema: "LON");
        }
    }
}
