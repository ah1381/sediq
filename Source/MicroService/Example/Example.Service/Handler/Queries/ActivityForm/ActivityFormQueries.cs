using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace Example.Service.Handler.Queries.ActivityForm
{
    public class GetActivityFormQuery : IRequest<ActivityFormResponseDto?>
    {
        public int Id { get; set; }
    }

    public class GetActivityFormHandler : IRequestHandler<GetActivityFormQuery, ActivityFormResponseDto?>
    {
        private readonly IGenericRepository<ActivityFormEntity> _repository;
        private readonly IMapper _mapper;

        public GetActivityFormHandler(IGenericRepository<ActivityFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ActivityFormResponseDto?> Handle(GetActivityFormQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetQueryable()
                .Include(a => a.SelectedProgram)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(a => a.RowId == request.Id, cancellationToken);
            
            return entity == null ? null : _mapper.Map<ActivityFormResponseDto>(entity);
        }
    }

    public class GetAllActivityFormsQuery : IRequest<IEnumerable<ActivityFormResponseDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllActivityFormsHandler : IRequestHandler<GetAllActivityFormsQuery, IEnumerable<ActivityFormResponseDto>>
    {
        private readonly IGenericRepository<ActivityFormEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllActivityFormsHandler(IGenericRepository<ActivityFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActivityFormResponseDto>> Handle(GetAllActivityFormsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetQueryable()
                .Include(a => a.SelectedProgram)
                .Include(a => a.Student)
                .ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ActivityFormResponseDto>>(entities);
        }
    }
}

