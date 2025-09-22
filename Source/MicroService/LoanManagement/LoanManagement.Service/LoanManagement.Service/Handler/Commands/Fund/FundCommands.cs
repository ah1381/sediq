using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.Fund
{
    public class CreateFundCommand : IRequest<FundDto>
    {
        public FundDto Fund { get; set; }

        public CreateFundCommand(FundDto fund)
        {
            Fund = fund;
        }
    }

    public class CreateFundCommandHandler : IRequestHandler<CreateFundCommand, FundDto>
    {
        private readonly AppDbContext _context;

        public CreateFundCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundDto> Handle(CreateFundCommand request, CancellationToken cancellationToken)
        {
            var entity = new FundEntity
            {
                Name = request.Fund.Name,
                FundType = request.Fund.FundType,
                CalculationType = request.Fund.CalculationType,
                Region = request.Fund.Region,
                Province = request.Fund.Province,
                Level = request.Fund.Level,
                CreatedBy = request.Fund.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.Fund.Status,
                RandId = request.Fund.RandId
            };

            _context.Funds.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.Fund.RowId = entity.RowId;
            return request.Fund;
        }
    }

    public class UpdateFundCommand : IRequest<FundDto>
    {
        public FundDto Fund { get; set; }

        public UpdateFundCommand(FundDto fund)
        {
            Fund = fund;
        }
    }

    public class UpdateFundCommandHandler : IRequestHandler<UpdateFundCommand, FundDto>
    {
        private readonly AppDbContext _context;

        public UpdateFundCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundDto> Handle(UpdateFundCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Funds
                .FirstOrDefaultAsync(f => f.RowId == request.Fund.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Fund with RowId {request.Fund.RowId} not found.");
            }

            entity.Name = request.Fund.Name;
            entity.FundType = request.Fund.FundType;
            entity.CalculationType = request.Fund.CalculationType;
            entity.Region = request.Fund.Region;
            entity.Province = request.Fund.Province;
            entity.Level = request.Fund.Level;
            entity.CreatedBy = request.Fund.CreatedBy;
            entity.Status = request.Fund.Status;
            entity.RandId = request.Fund.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.Fund;
        }
    }

    public class DeleteFundCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteFundCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteFundCommandHandler : IRequestHandler<DeleteFundCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteFundCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFundCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Funds
                .FirstOrDefaultAsync(f => f.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.Funds.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}