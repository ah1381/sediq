using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handlers.Commands.Notification
{
    public class CreateNotificationCommand : IRequest<NotificationDto>
    {
        public NotificationDto Notification { get; set; }

        public CreateNotificationCommand(NotificationDto notification)
        {
            Notification = notification;
        }
    }

    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, NotificationDto>
    {
        private readonly AppDbContext _context;

        public CreateNotificationCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NotificationDto> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var entity = new NotificationEntity
            {
                PersonnelID = request.Notification.PersonnelId,
                NotificationType = request.Notification.NotificationType,
                Message = request.Notification.Message,
                NotificationDate = request.Notification.NotificationDate,
                Channel = request.Notification.Channel,
                Read = request.Notification.Read,
                CreatedBy = request.Notification.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                RevSeq = (short)1,
                Status = request.Notification.Status,
                RandId = request.Notification.RandId
            };

            _context.Notifications.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.Notification.RowId = entity.RowId;
            return request.Notification;
        }
    }

    public class UpdateNotificationCommand : IRequest<NotificationDto>
    {
        public NotificationDto Notification { get; set; }

        public UpdateNotificationCommand(NotificationDto notification)
        {
            Notification = notification;
        }
    }

    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, NotificationDto>
    {
        private readonly AppDbContext _context;

        public UpdateNotificationCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NotificationDto> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notifications
                .FirstOrDefaultAsync(n => n.RowId == request.Notification.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Notification with RowId {request.Notification.RowId} not found.");
            }

            entity.PersonnelID = request.Notification.PersonnelId;
            entity.NotificationType = request.Notification.NotificationType;
            entity.Message = request.Notification.Message;
            entity.NotificationDate = request.Notification.NotificationDate;
            entity.Channel = request.Notification.Channel;
            entity.Read = request.Notification.Read;
            entity.CreatedBy = request.Notification.CreatedBy;
            entity.Status = request.Notification.Status;
            entity.RandId = request.Notification.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq = (short)(entity.RevSeq + 1);

            await _context.SaveChangesAsync(cancellationToken);

            return request.Notification;
        }
    }

    public class DeleteNotificationCommand : IRequest<bool>
    {
        public long? RowId { get; set; } // Changed to long? to match BaseEntity

        public DeleteNotificationCommand(long? rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteNotificationCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            if (!request.RowId.HasValue)
            {
                return false;
            }

            var entity = await _context.Notifications
                .FirstOrDefaultAsync(n => n.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.Notifications.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}