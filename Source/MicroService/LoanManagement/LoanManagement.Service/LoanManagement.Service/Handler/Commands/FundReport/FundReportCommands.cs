using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.FundReport
{
    public class CreateFundReportCommand : IRequest<FundReportDto>
    {
        public FundReportDto FundReport { get; set; }

        public CreateFundReportCommand(FundReportDto fundReport)
        {
            FundReport = fundReport;
        }
    }

    public class CreateFundReportCommandHandler : IRequestHandler<CreateFundReportCommand, FundReportDto>
    {
        private readonly AppDbContext _context;

        public CreateFundReportCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundReportDto> Handle(CreateFundReportCommand request, CancellationToken cancellationToken)
        {
            var entity = new FundReportEntity
            {
                FundID = request.FundReport.FundId,
                ReportYear = request.FundReport.ReportYear,
                ReportType = request.FundReport.ReportType,
                Balance = request.FundReport.Balance,
                Description = request.FundReport.Description,
                CreatedBy = request.FundReport.CreatedBy,
                Approved = request.FundReport.Approved,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.FundReport.Status,
                RandId = request.FundReport.RandId
            };

            _context.FundReports.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.FundReport.RowId = entity.RowId;
            return request.FundReport;
        }
    }

    public class UpdateFundReportCommand : IRequest<FundReportDto>
    {
        public FundReportDto FundReport { get; set; }

        public UpdateFundReportCommand(FundReportDto fundReport)
        {
            FundReport = fundReport;
        }
    }

    public class UpdateFundReportCommandHandler : IRequestHandler<UpdateFundReportCommand, FundReportDto>
    {
        private readonly AppDbContext _context;

        public UpdateFundReportCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundReportDto> Handle(UpdateFundReportCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundReports
                .FirstOrDefaultAsync(fr => fr.RowId == request.FundReport.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"FundReport with RowId {request.FundReport.RowId} not found.");
            }

            entity.FundID = request.FundReport.FundId;
            entity.ReportYear = request.FundReport.ReportYear;
            entity.ReportType = request.FundReport.ReportType;
            entity.Balance = request.FundReport.Balance;
            entity.Description = request.FundReport.Description;
            entity.CreatedBy = request.FundReport.CreatedBy;
            entity.Approved = request.FundReport.Approved;
            entity.Status = request.FundReport.Status;
            entity.RandId = request.FundReport.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.FundReport;
        }
    }

    public class DeleteFundReportCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteFundReportCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteFundReportCommandHandler : IRequestHandler<DeleteFundReportCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteFundReportCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFundReportCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundReports
                .FirstOrDefaultAsync(fr => fr.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.FundReports.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}