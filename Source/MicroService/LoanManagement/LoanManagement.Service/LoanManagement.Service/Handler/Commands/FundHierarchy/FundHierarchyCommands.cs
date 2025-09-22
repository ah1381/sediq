using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.FundHierarchy
{
    public class CreateFundHierarchyCommand : IRequest<FundHierarchyDto>
    {
        public FundHierarchyDto FundHierarchy { get; set; }

        public CreateFundHierarchyCommand(FundHierarchyDto fundHierarchy)
        {
            FundHierarchy = fundHierarchy;
        }
    }

    public class CreateFundHierarchyCommandHandler : IRequestHandler<CreateFundHierarchyCommand, FundHierarchyDto>
    {
        private readonly AppDbContext _context;

        public CreateFundHierarchyCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundHierarchyDto> Handle(CreateFundHierarchyCommand request, CancellationToken cancellationToken)
        {
            var entity = new FundHierarchyEntity
            {
                FundID = request.FundHierarchy.FundId,
                ParentFundID = request.FundHierarchy.ParentFundId,
                Description = request.FundHierarchy.Description,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.FundHierarchy.Status,
                RandId = request.FundHierarchy.RandId
            };

            _context.FundHierarchies.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.FundHierarchy.RowId = entity.RowId;
            return request.FundHierarchy;
        }
    }

    public class UpdateFundHierarchyCommand : IRequest<FundHierarchyDto>
    {
        public FundHierarchyDto FundHierarchy { get; set; }

        public UpdateFundHierarchyCommand(FundHierarchyDto fundHierarchy)
        {
            FundHierarchy = fundHierarchy;
        }
    }

    public class UpdateFundHierarchyCommandHandler : IRequestHandler<UpdateFundHierarchyCommand, FundHierarchyDto>
    {
        private readonly AppDbContext _context;

        public UpdateFundHierarchyCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundHierarchyDto> Handle(UpdateFundHierarchyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundHierarchies
                .FirstOrDefaultAsync(fh => fh.RowId == request.FundHierarchy.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"FundHierarchy with RowId {request.FundHierarchy.RowId} not found.");
            }

            entity.FundID = request.FundHierarchy.FundId;
            entity.ParentFundID = request.FundHierarchy.ParentFundId;
            entity.Description = request.FundHierarchy.Description;
            entity.Status = request.FundHierarchy.Status;
            entity.RandId = request.FundHierarchy.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.FundHierarchy;
        }
    }

    public class DeleteFundHierarchyCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteFundHierarchyCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteFundHierarchyCommandHandler : IRequestHandler<DeleteFundHierarchyCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteFundHierarchyCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFundHierarchyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundHierarchies
                .FirstOrDefaultAsync(fh => fh.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.FundHierarchies.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}