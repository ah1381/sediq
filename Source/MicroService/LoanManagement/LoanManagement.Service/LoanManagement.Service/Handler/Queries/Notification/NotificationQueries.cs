using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.Notification
{
    public class GetNotificationByIdQuery : IRequest<NotificationDto?>
    {
        public long Id { get; set; }

        public GetNotificationByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetNotificationByIdHandler : IRequestHandler<GetNotificationByIdQuery, NotificationDto?>
    {
        private readonly IGenericRepository<NotificationEntity> _repository;
        private readonly IMapper _mapper;

        public GetNotificationByIdHandler(IGenericRepository<NotificationEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<NotificationDto?> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<NotificationDto>(entity);
        }
    }

    public class GetAllNotificationsQuery : IRequest<List<NotificationDto>>
    {
    }

    public class GetAllNotificationsHandler : IRequestHandler<GetAllNotificationsQuery, List<NotificationDto>>
    {
        private readonly IGenericRepository<NotificationEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllNotificationsHandler(IGenericRepository<NotificationEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<NotificationDto>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<NotificationDto>>(entities);
        }
    }
}
