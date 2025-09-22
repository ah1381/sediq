using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.FundRegion
{
    public class CreateFundRegionCommand : IRequest<FundRegionDto>
    {
        public FundRegionDto FundRegion { get; set; }

        public CreateFundRegionCommand(FundRegionDto fundRegion)
        {
            FundRegion = fundRegion;
        }
    }

    public class CreateFundRegionCommandHandler : IRequestHandler<CreateFundRegionCommand, FundRegionDto>
    {
        private readonly AppDbContext _context;

        public CreateFundRegionCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundRegionDto> Handle(CreateFundRegionCommand request, CancellationToken cancellationToken)
        {
            var entity = new FundRegionEntity
            {
                FundID = request.FundRegion.FundId,
                RegionName = request.FundRegion.RegionName,
                ActiveMembers = request.FundRegion.ActiveMembers,
                RetiredMembers = request.FundRegion.RetiredMembers,
                BankName = request.FundRegion.BankName,
                BranchCode = request.FundRegion.BranchCode,
                OrgCode = request.FundRegion.OrgCode,
                InstallmentDeductionCodes = request.FundRegion.InstallmentDeductionCodes,
                ShareDeductionCodes = request.FundRegion.ShareDeductionCodes,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.FundRegion.Status,
                RandId = request.FundRegion.RandId
            };

            _context.FundRegions.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.FundRegion.RowId = entity.RowId;
            return request.FundRegion;
        }
    }

    public class UpdateFundRegionCommand : IRequest<FundRegionDto>
    {
        public FundRegionDto FundRegion { get; set; }

        public UpdateFundRegionCommand(FundRegionDto fundRegion)
        {
            FundRegion = fundRegion;
        }
    }

    public class UpdateFundRegionCommandHandler : IRequestHandler<UpdateFundRegionCommand, FundRegionDto>
    {
        private readonly AppDbContext _context;

        public UpdateFundRegionCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundRegionDto> Handle(UpdateFundRegionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundRegions
                .FirstOrDefaultAsync(fr => fr.RowId == request.FundRegion.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"FundRegion with RowId {request.FundRegion.RowId} not found.");
            }

            entity.FundID = request.FundRegion.FundId;
            entity.RegionName = request.FundRegion.RegionName;
            entity.ActiveMembers = request.FundRegion.ActiveMembers;
            entity.RetiredMembers = request.FundRegion.RetiredMembers;
            entity.BankName = request.FundRegion.BankName;
            entity.BranchCode = request.FundRegion.BranchCode;
            entity.OrgCode = request.FundRegion.OrgCode;
            entity.InstallmentDeductionCodes = request.FundRegion.InstallmentDeductionCodes;
            entity.ShareDeductionCodes = request.FundRegion.ShareDeductionCodes;
            entity.Status = request.FundRegion.Status;
            entity.RandId = request.FundRegion.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.FundRegion;
        }
    }

    public class DeleteFundRegionCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteFundRegionCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteFundRegionCommandHandler : IRequestHandler<DeleteFundRegionCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteFundRegionCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFundRegionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundRegions
                .FirstOrDefaultAsync(fr => fr.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.FundRegions.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}