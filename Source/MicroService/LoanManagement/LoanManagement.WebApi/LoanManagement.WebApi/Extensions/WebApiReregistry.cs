using FluentValidation.AspNetCore;
using LoanManagement.Service.Handler.Commands.Fund;
using LoanManagement.Service.Handler.Commands.FundElection;
using LoanManagement.Service.Handler.Commands.FundExecutive;
using LoanManagement.Service.Handler.Commands.FundHierarchy;
using LoanManagement.Service.Handler.Commands.FundInspector;
using LoanManagement.Service.Handler.Commands.FundInsurance;
using LoanManagement.Service.Handler.Commands.FundMember;
using LoanManagement.Service.Handler.Commands.FundRegion;
using LoanManagement.Service.Handler.Commands.FundReport;
using LoanManagement.Service.Handler.Commands.FundSetting;
using LoanManagement.Service.Handler.Commands.FundTransaction;
using LoanManagement.Service.Handler.Commands.FundTransfer;
using LoanManagement.Service.Handler.Commands.LoanAdjustmentRequest;
using LoanManagement.Service.Handler.Commands.LoanCertificate;
using LoanManagement.Service.Handler.Commands.LoanGuarantor;
using LoanManagement.Service.Handler.Commands.LoanRepayment;
using LoanManagement.Service.Handler.Commands.LoanRequest;
using LoanManagement.Service.Handler.Commands.LoanRequestLog;
using LoanManagement.Service.Handler.Commands.LoanType;
using LoanManagement.Service.Handler.Commands.Personnel;
using LoanManagement.Service.Handler.Queries.Fund;
using LoanManagement.Service.Handler.Queries.FundElection;
using LoanManagement.Service.Handler.Queries.FundExecutive;
using LoanManagement.Service.Handler.Queries.FundHierarchy;
using LoanManagement.Service.Handler.Queries.FundInspector;
using LoanManagement.Service.Handler.Queries.FundInsurance;
using LoanManagement.Service.Handler.Queries.FundMember;
using LoanManagement.Service.Handler.Queries.FundRegion;
using LoanManagement.Service.Handler.Queries.FundReport;
using LoanManagement.Service.Handler.Queries.FundSetting;
using LoanManagement.Service.Handler.Queries.FundTransaction;
using LoanManagement.Service.Handler.Queries.FundTransfer;
using LoanManagement.Service.Handler.Queries.LoanAdjustmentRequest;
using LoanManagement.Service.Handler.Queries.LoanCertificate;
using LoanManagement.Service.Handler.Queries.LoanGuarantor;
using LoanManagement.Service.Handler.Queries.LoanRepayment;
using LoanManagement.Service.Handler.Queries.LoanRequest;
using LoanManagement.Service.Handler.Queries.LoanRequestLog;
using LoanManagement.Service.Handler.Queries.LoanType;
using LoanManagement.Service.Handler.Queries.Notification;
using LoanManagement.Service.Handler.Queries.Personnel;
using LoanManagement.Service.Handlers.Commands.Notification;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Raya.Hrm.Shared.Library.GeneralErrorService;
using Raya.Hrm.Shared.Library.GeneralRequestService;
using Raya.Hrm.Shared.Library.Models.Auth;
using Raya.Hrm.Shared.Library.Models.Configs;

namespace LoanManagement.WebApi.Extensions
{
    public static class WebApiReregistry
    {
        public static IServiceCollection AddWebApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // JWT
            services.Configure<JwtConfig>(configuration.GetSection("Jwt"));

            // MongoDB
            //services.Configure<MongoDbConfig>(configuration.GetSection("MongoDb"));

            //services.AddSingleton<IMongoClient>(sp =>
            //{
            //    var mongoConfig = sp.GetRequiredService<IOptions<MongoDbConfig>>().Value;
            //    return new MongoClient(mongoConfig.ConnectionString);
            //});

            //services.AddScoped<IMongoDatabase>(sp =>
            //{
            //    var mongoConfig = sp.GetRequiredService<IOptions<MongoDbConfig>>().Value;
            //    var client = sp.GetRequiredService<IMongoClient>();
            //    return client.GetDatabase(mongoConfig.ConnectionString);
            //});

            // Register Mongo logging services
            //services.AddSingleton<IErrorMongoService, ErrorMongoService>();
            //services.AddSingleton<IRequestMongoService, RequestMongoService>();

            // MediatR
            services.AddMediatR(cfg =>
            {
                // Personnel Commands
                cfg.RegisterServicesFromAssembly(typeof(CreatePersonnelCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdatePersonnelCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeletePersonnelCommand).Assembly);

                // Fund Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateFundCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateFundCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteFundCommand).Assembly);

                // FundHierarchy Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateFundHierarchyCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateFundHierarchyCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteFundHierarchyCommand).Assembly);

                // FundRegion Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateFundRegionCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateFundRegionCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteFundRegionCommand).Assembly);

                // FundMember Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateFundMemberCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateFundMemberCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteFundMemberCommand).Assembly);

                // FundExecutive Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateFundExecutiveCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateFundExecutiveCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteFundExecutiveCommand).Assembly);

                // FundInspector Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateFundInspectorCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateFundInspectorCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteFundInspectorCommand).Assembly);

                // FundSetting Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateFundSettingCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateFundSettingCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteFundSettingCommand).Assembly);

                // FundTransaction Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateFundTransactionCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateFundTransactionCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteFundTransactionCommand).Assembly);

                // LoanAdjustmentRequest Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateLoanAdjustmentRequestCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateLoanAdjustmentRequestCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteLoanAdjustmentRequestCommand).Assembly);

                // LoanRequest Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateLoanRequestCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateLoanRequestCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteLoanRequestCommand).Assembly);

                // LoanRequestLog Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateLoanRequestLogCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateLoanRequestLogCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteLoanRequestLogCommand).Assembly);

                // LoanType Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateLoanTypeCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateLoanTypeCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteLoanTypeCommand).Assembly);

                // LoanRepayment Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateLoanRepaymentCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateLoanRepaymentCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteLoanRepaymentCommand).Assembly);

                // LoanCertificate Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateLoanCertificateCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateLoanCertificateCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteLoanCertificateCommand).Assembly);

                // LoanGuarantor Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateLoanGuarantorCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateLoanGuarantorCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteLoanGuarantorCommand).Assembly);

                // FundElection Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateFundElectionCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateFundElectionCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteFundElectionCommand).Assembly);

                // FundTransfer Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateFundTransferCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateFundTransferCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteFundTransferCommand).Assembly);

                // FundInsurance Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateFundInsuranceCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateFundInsuranceCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteFundInsuranceCommand).Assembly);

                // FundReport Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateFundReportCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateFundReportCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteFundReportCommand).Assembly);

                // Notification Commands
                cfg.RegisterServicesFromAssembly(typeof(CreateNotificationCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateNotificationCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteNotificationCommand).Assembly);

                // Personnel Queries
                cfg.RegisterServicesFromAssembly(typeof(GetPersonnelByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllPersonnelQuery).Assembly);

                // Fund Queries
                cfg.RegisterServicesFromAssembly(typeof(GetFundByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllFundsQuery).Assembly);

                // FundHierarchy Queries
                cfg.RegisterServicesFromAssembly(typeof(GetFundHierarchyByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllFundHierarchiesQuery).Assembly);

                // FundRegion Queries
                cfg.RegisterServicesFromAssembly(typeof(GetFundRegionByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllFundRegionsQuery).Assembly);

                // FundMember Queries
                cfg.RegisterServicesFromAssembly(typeof(GetFundMemberByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllFundMembersQuery).Assembly);

                // FundExecutive Queries
                cfg.RegisterServicesFromAssembly(typeof(GetFundExecutiveByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllFundExecutivesQuery).Assembly);

                // FundInspector Queries
                cfg.RegisterServicesFromAssembly(typeof(GetFundInspectorByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllFundInspectorsQuery).Assembly);

                // FundSetting Queries
                cfg.RegisterServicesFromAssembly(typeof(GetFundSettingByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllFundSettingsQuery).Assembly);

                // FundTransaction Queries
                cfg.RegisterServicesFromAssembly(typeof(GetFundTransactionByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllFundTransactionsQuery).Assembly);

                // LoanAdjustmentRequest Queries
                cfg.RegisterServicesFromAssembly(typeof(GetLoanAdjustmentRequestByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllLoanAdjustmentRequestsQuery).Assembly);

                // LoanRequest Queries
                cfg.RegisterServicesFromAssembly(typeof(GetLoanRequestByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllLoanRequestsQuery).Assembly);

                // LoanRequestLog Queries
                cfg.RegisterServicesFromAssembly(typeof(GetLoanRequestLogByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllLoanRequestLogsQuery).Assembly);

                // LoanType Queries
                cfg.RegisterServicesFromAssembly(typeof(GetLoanTypeByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllLoanTypesQuery).Assembly);

                // LoanRepayment Queries
                cfg.RegisterServicesFromAssembly(typeof(GetLoanRepaymentByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllLoanRepaymentsQuery).Assembly);

                // LoanCertificate Queries
                cfg.RegisterServicesFromAssembly(typeof(GetLoanCertificateByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllLoanCertificatesQuery).Assembly);

                // LoanGuarantor Queries
                cfg.RegisterServicesFromAssembly(typeof(GetLoanGuarantorByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllLoanGuarantorsQuery).Assembly);

                // FundElection Queries
                cfg.RegisterServicesFromAssembly(typeof(GetFundElectionByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllFundElectionsQuery).Assembly);

                // FundTransfer Queries
                cfg.RegisterServicesFromAssembly(typeof(GetFundTransferByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllFundTransfersQuery).Assembly);

                // FundInsurance Queries
                cfg.RegisterServicesFromAssembly(typeof(GetFundInsuranceByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllFundInsurancesQuery).Assembly);

                // FundReport Queries
                cfg.RegisterServicesFromAssembly(typeof(GetFundReportByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllFundReportsQuery).Assembly);

                // Notification Queries
                cfg.RegisterServicesFromAssembly(typeof(GetNotificationByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllNotificationsQuery).Assembly);
            });

            // Controllers + FluentValidation
            services.AddControllers()
                    .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<Program>();
                    });

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Title = "LoanManagement API",
                    Version = "v1",
                    Description = "A Clean Architecture API with CQRS, EF Core, Dapper, MediatR, and MongoDB"
                });
            });

            // CORS
            var corsConfig = configuration.GetSection("Cors").Get<CorsConfig>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder.WithOrigins(corsConfig.AllowedOrigins)
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            return services;
        }

        public class CorsConfig
        {
            public string[] AllowedOrigins { get; set; }
        }
    }
}
