using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.Personnel
{
    public class CreatePersonnelCommand : IRequest<PersonnelDto>
    {
        public PersonnelDto Personnel { get; set; }

        public CreatePersonnelCommand()
        {

        }

        public CreatePersonnelCommand(PersonnelDto personnel)
        {
            Personnel = personnel;
        }
    }

    public class CreatePersonnelCommandHandler : IRequestHandler<CreatePersonnelCommand, PersonnelDto>
    {
        private readonly AppDbContext _context;

        public CreatePersonnelCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PersonnelDto> Handle(CreatePersonnelCommand request, CancellationToken cancellationToken)
        {
            var entity = new PersonnelEntity
            {
                FirstName = request.Personnel.FirstName,
                LastName = request.Personnel.LastName,
                NationalCode = request.Personnel.NationalCode,
                EmploymentCode = request.Personnel.EmploymentCode,
                BirthDate = request.Personnel.BirthDate,
                Phone = request.Personnel.Phone,
                Mobile = request.Personnel.Mobile,
                Address = request.Personnel.Address,
                Email = request.Personnel.Email,
                FatherName = request.Personnel.FatherName,
                Department = request.Personnel.Department,
                Province = request.Personnel.Province,
                EmploymentType = request.Personnel.EmploymentType,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.Personnel.Status,
                RandId = request.Personnel.RandId
            };

            _context.Personnel.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.Personnel.RowId = entity.RowId;
            return request.Personnel;
        }
    }

    public class UpdatePersonnelCommand : IRequest<PersonnelDto>
    {
        public PersonnelDto Personnel { get; set; }

        public UpdatePersonnelCommand()
        {

        }

        public UpdatePersonnelCommand(PersonnelDto personnel)
        {
            Personnel = personnel;
        }
    }

    public class UpdatePersonnelCommandHandler : IRequestHandler<UpdatePersonnelCommand, PersonnelDto>
    {
        private readonly AppDbContext _context;

        public UpdatePersonnelCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PersonnelDto> Handle(UpdatePersonnelCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Personnel
                .FirstOrDefaultAsync(p => p.RowId == request.Personnel.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Personnel with RowId {request.Personnel.RowId} not found.");
            }

            entity.FirstName = request.Personnel.FirstName;
            entity.LastName = request.Personnel.LastName;
            entity.NationalCode = request.Personnel.NationalCode;
            entity.EmploymentCode = request.Personnel.EmploymentCode;
            entity.BirthDate = request.Personnel.BirthDate;
            entity.Phone = request.Personnel.Phone;
            entity.Mobile = request.Personnel.Mobile;
            entity.Address = request.Personnel.Address;
            entity.Email = request.Personnel.Email;
            entity.FatherName = request.Personnel.FatherName;
            entity.Department = request.Personnel.Department;
            entity.Province = request.Personnel.Province;
            entity.EmploymentType = request.Personnel.EmploymentType;
            entity.Status = request.Personnel.Status;
            entity.RandId = request.Personnel.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.Personnel;
        }
    }

    public class DeletePersonnelCommand : IRequest<bool>
    {
        public long RowId { get; set; }
        public DeletePersonnelCommand()
        {
        }
        public DeletePersonnelCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeletePersonnelCommandHandler : IRequestHandler<DeletePersonnelCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeletePersonnelCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeletePersonnelCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Personnel
                .FirstOrDefaultAsync(p => p.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.Personnel.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
