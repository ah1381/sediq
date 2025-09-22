using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.FundInsurance
{
    public class CreateFundInsuranceCommand : IRequest<FundInsuranceDto>
    {
        public FundInsuranceDto FundInsurance { get; set; }

        public CreateFundInsuranceCommand(FundInsuranceDto fundInsurance)
        {
            FundInsurance = fundInsurance;
        }
    }

    public class CreateFundInsuranceCommandHandler : IRequestHandler<CreateFundInsuranceCommand, FundInsuranceDto>
    {
        private readonly AppDbContext _context;

        public CreateFundInsuranceCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundInsuranceDto> Handle(CreateFundInsuranceCommand request, CancellationToken cancellationToken)
        {
            var entity = new FundInsuranceEntity
            {
                FundID = request.FundInsurance.FundId,
                LoanRequestID = request.FundInsurance.LoanRequestId,
                InsuranceAmount = request.FundInsurance.InsuranceAmount,
                InsuranceDate = request.FundInsurance.InsuranceDate,
                Provider = request.FundInsurance.Provider,
                CreatedBy = request.FundInsurance.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.FundInsurance.Status,
                RandId = request.FundInsurance.RandId
            };

            _context.FundInsurances.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.FundInsurance.RowId = entity.RowId;
            return request.FundInsurance;
        }
    }

    public class UpdateFundInsuranceCommand : IRequest<FundInsuranceDto>
    {
        public FundInsuranceDto FundInsurance { get; set; }

        public UpdateFundInsuranceCommand(FundInsuranceDto fundInsurance)
        {
            FundInsurance = fundInsurance;
        }
    }

    public class UpdateFundInsuranceCommandHandler : IRequestHandler<UpdateFundInsuranceCommand, FundInsuranceDto>
    {
        private readonly AppDbContext _context;

        public UpdateFundInsuranceCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundInsuranceDto> Handle(UpdateFundInsuranceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundInsurances
                .FirstOrDefaultAsync(fi => fi.RowId == request.FundInsurance.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"FundInsurance with RowId {request.FundInsurance.RowId} not found.");
            }

            entity.FundID = request.FundInsurance.FundId;
            entity.LoanRequestID = request.FundInsurance.LoanRequestId;
            entity.InsuranceAmount = request.FundInsurance.InsuranceAmount;
            entity.InsuranceDate = request.FundInsurance.InsuranceDate;
            entity.Provider = request.FundInsurance.Provider;
            entity.CreatedBy = request.FundInsurance.CreatedBy;
            entity.Status = request.FundInsurance.Status;
            entity.RandId = request.FundInsurance.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.FundInsurance;
        }
    }

    public class DeleteFundInsuranceCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteFundInsuranceCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteFundInsuranceCommandHandler : IRequestHandler<DeleteFundInsuranceCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteFundInsuranceCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFundInsuranceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundInsurances
                .FirstOrDefaultAsync(fi => fi.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.FundInsurances.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
