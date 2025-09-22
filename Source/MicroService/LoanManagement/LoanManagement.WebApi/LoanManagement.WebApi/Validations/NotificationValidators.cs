using FluentValidation;
using LoanManagement.Service.Handlers.Commands.Notification;

namespace LoanManagement.WebApi.Validations
{
    public class CreateNotificationValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationValidator()
        {
            RuleFor(x => x.Notification)
                .NotNull()
                .WithMessage("Notification data is required.");

            RuleFor(x => x.Notification.PersonnelId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("PersonnelId must be greater than 0.");

            RuleFor(x => x.Notification.Message)
                .NotEmpty()
                .WithMessage("Message is required.");

            RuleFor(x => x.Notification.NotificationDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("NotificationDate must be on or before the current date.");

            RuleFor(x => x.Notification.NotificationType)
                .MaximumLength(50)
                .WithMessage("NotificationType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Notification.NotificationType));

            RuleFor(x => x.Notification.Channel)
                .MaximumLength(50)
                .WithMessage("Channel must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Notification.Channel));

            RuleFor(x => x.Notification.Status)
                .InclusiveBetween((short)0, short.MaxValue)
                .WithMessage("Status must be a valid positive short value.");
        }
    }

    public class UpdateNotificationValidator : AbstractValidator<UpdateNotificationCommand>
    {
        public UpdateNotificationValidator()
        {
            RuleFor(x => x.Notification)
                .NotNull()
                .WithMessage("Notification data is required.");

            RuleFor(x => x.Notification.RowId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.Notification.PersonnelId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("PersonnelId must be greater than 0.");

            RuleFor(x => x.Notification.Message)
                .NotEmpty()
                .WithMessage("Message is required.")
                .When(x => !string.IsNullOrEmpty(x.Notification.Message));

            RuleFor(x => x.Notification.NotificationDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("NotificationDate must be on or before the current date.")
                .When(x => x.Notification.NotificationDate != default);

            RuleFor(x => x.Notification.NotificationType)
                .MaximumLength(50)
                .WithMessage("NotificationType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Notification.NotificationType));

            RuleFor(x => x.Notification.Channel)
                .MaximumLength(50)
                .WithMessage("Channel must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Notification.Channel));

            RuleFor(x => x.Notification.Status)
                .InclusiveBetween((short)0, short.MaxValue)
                .WithMessage("Status must be a valid positive short value.");
        }
    }

    public class DeleteNotificationValidator : AbstractValidator<DeleteNotificationCommand>
    {
        public DeleteNotificationValidator()
        {
            RuleFor(x => x.RowId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}