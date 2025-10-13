using System;
using System.Collections.Generic;
using System.Linq;

namespace Raya.Hrm.Shared.Library.Models.Exception
{
    public class BpcValidationException : System.Exception, IBpcHttpException
    {
        public List<CustomError> CustomErrors { get; }
        public List<ValidationError> ValidationErrors { get; }
        public int StatusCode => 603;

        public BpcValidationException(List<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors;
            CustomErrors = validationErrors?.Select(x => new CustomError(-1, x.Message, null, x.PropertyName)).ToList();
        }

        public BpcValidationException(string propertyName, string message)
        {
            ValidationErrors = new List<ValidationError>
            {
                new ValidationError(propertyName, message)
            };
            CustomErrors = new List<CustomError>
            {
                new CustomError(-1, message, null, propertyName)
            };
        }
    }

    public class ValidationError
    {
        public string PropertyName { get; }
        public string Message { get; }

        public ValidationError(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }
    }

    public class CustomError
    {
        public int Code { get; }
        public string Message { get; }
        public string PropertyName { get; }
        public string Description { get; }

        public CustomError(int Code, string message, string Description = null, string propertyName = null)
        {
            this.Code = Code;
            Message = message;
            PropertyName = propertyName;
            this.Description = Description;
        }
    }

    public interface IBpcHttpException
    {
        int StatusCode { get; }
    }
}
