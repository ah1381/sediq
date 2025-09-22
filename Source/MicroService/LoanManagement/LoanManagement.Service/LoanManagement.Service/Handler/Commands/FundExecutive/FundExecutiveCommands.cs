using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.FundExecutive
{
    public class CreateFundExecutiveCommand : IRequest<FundExecutiveDto>
    {
        public FundExecutiveDto FundExecutive { get; set; }

        public CreateFundExecutiveCommand(FundExecutiveDto fundExecutive)
        {
            FundExecutive = fundExecutive;
        }
    }

    public class CreateFundExecutiveCommandHandler : IRequestHandler<CreateFundExecutiveCommand, FundExecutiveDto>
    {
        private readonly AppDbContext _context;

        public CreateFundExecutiveCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundExecutiveDto> Handle(CreateFundExecutiveCommand request, CancellationToken cancellationToken)
        {
            var entity = new FundExecutiveEntity
            {
                FundID = request.FundExecutive.FundId,
                PersonnelID = request.FundExecutive.PersonnelId,
                Role = request.FundExecutive.Role,
                StartDate = request.FundExecutive.StartDate,
                EndDate = request.FundExecutive.EndDate,
                HasSignPermission = request.FundExecutive.HasSignPermission,
                CreatedBy = request.FundExecutive.CreatedBy,
                OrderNumber = request.FundExecutive.OrderNumber,
                Elected = request.FundExecutive.Elected,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.FundExecutive.Status,
                RandId = request.FundExecutive.RandId
            };

            _context.FundExecutives.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.FundExecutive.RowId = entity.RowId;
            return request.FundExecutive;
        }
    }

    public class UpdateFundExecutiveCommand : IRequest<FundExecutiveDto>
    {
        public FundExecutiveDto FundExecutive { get; set; }

        public UpdateFundExecutiveCommand(FundExecutiveDto fundExecutive)
        {
            FundExecutive = fundExecutive;
        }
    }

    public class UpdateFundExecutiveCommandHandler : IRequestHandler<UpdateFundExecutiveCommand, FundExecutiveDto>
    {
        private readonly AppDbContext _context;

        public UpdateFundExecutiveCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundExecutiveDto> Handle(UpdateFundExecutiveCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundExecutives
                .FirstOrDefaultAsync(fe => fe.RowId == request.FundExecutive.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"FundExecutive with RowId {request.FundExecutive.RowId} not found.");
            }

            entity.FundID = request.FundExecutive.FundId;
            entity.PersonnelID = request.FundExecutive.PersonnelId;
            entity.Role = request.FundExecutive.Role;
            entity.StartDate = request.FundExecutive.StartDate;
            entity.EndDate = request.FundExecutive.EndDate;
            entity.HasSignPermission = request.FundExecutive.HasSignPermission;
            entity.CreatedBy = request.FundExecutive.CreatedBy;
            entity.OrderNumber = request.FundExecutive.OrderNumber;
            entity.Elected = request.FundExecutive.Elected;
            entity.Status = request.FundExecutive.Status;
            entity.RandId = request.FundExecutive.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.FundExecutive;
        }
    }

    public class DeleteFundExecutiveCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteFundExecutiveCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteFundExecutiveCommandHandler : IRequestHandler<DeleteFundExecutiveCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteFundExecutiveCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFundExecutiveCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundExecutives
                .FirstOrDefaultAsync(fe => fe.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.FundExecutives.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
