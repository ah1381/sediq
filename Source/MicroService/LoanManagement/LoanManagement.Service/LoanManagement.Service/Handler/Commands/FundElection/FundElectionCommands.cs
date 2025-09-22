using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.FundElection
{
    public class CreateFundElectionCommand : IRequest<FundElectionDto>
    {
        public FundElectionDto FundElection { get; set; }

        public CreateFundElectionCommand(FundElectionDto fundElection)
        {
            FundElection = fundElection;
        }
    }

    public class CreateFundElectionCommandHandler : IRequestHandler<CreateFundElectionCommand, FundElectionDto>
    {
        private readonly AppDbContext _context;

        public CreateFundElectionCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundElectionDto> Handle(CreateFundElectionCommand request, CancellationToken cancellationToken)
        {
            var entity = new FundElectionEntity
            {
                FundID = request.FundElection.FundId,
                ElectionDate = request.FundElection.ElectionDate,
                CandidateID = request.FundElection.CandidateId,
                Votes = request.FundElection.Votes,
                Position = request.FundElection.Position,
                CreatedBy = request.FundElection.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                FundStatus = request.FundElection.Status,
                RandId = request.FundElection.RandId
            };

            _context.FundElections.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.FundElection.RowId = entity.RowId;
            return request.FundElection;
        }
    }

    public class UpdateFundElectionCommand : IRequest<FundElectionDto>
    {
        public FundElectionDto FundElection { get; set; }

        public UpdateFundElectionCommand(FundElectionDto fundElection)
        {
            FundElection = fundElection;
        }
    }

    public class UpdateFundElectionCommandHandler : IRequestHandler<UpdateFundElectionCommand, FundElectionDto>
    {
        private readonly AppDbContext _context;

        public UpdateFundElectionCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundElectionDto> Handle(UpdateFundElectionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundElections
                .FirstOrDefaultAsync(fe => fe.RowId == request.FundElection.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"FundElection with RowId {request.FundElection.RowId} not found.");
            }

            entity.FundID = request.FundElection.FundId;
            entity.ElectionDate = request.FundElection.ElectionDate;
            entity.CandidateID = request.FundElection.CandidateId;
            entity.Votes = request.FundElection.Votes;
            entity.Position = request.FundElection.Position;
            entity.CreatedBy = request.FundElection.CreatedBy;
            entity.FundStatus = request.FundElection.Status;
            entity.RandId = request.FundElection.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.FundElection;
        }
    }

    public class DeleteFundElectionCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteFundElectionCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteFundElectionCommandHandler : IRequestHandler<DeleteFundElectionCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteFundElectionCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFundElectionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundElections
                .FirstOrDefaultAsync(fe => fe.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.FundElections.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}