using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Sediq.Web.Api.ModelBinders
{
    public class PersianDateModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
                return Task.CompletedTask;

            try
            {
                // تبدیل اعداد فارسی به انگلیسی
                value = value.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3")
                             .Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7")
                             .Replace("۸", "8").Replace("۹", "9");
                
                var persianCalendar = new PersianCalendar();
                var parts = value.Split('/');
                
                if (parts.Length == 3)
                {
                    int year = int.Parse(parts[0]);
                    int month = int.Parse(parts[1]);
                    int day = int.Parse(parts[2]);
                    
                    // بررسی محدوده ماه
                    if (month < 1 || month > 12)
                    {
                        bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"ماه باید بین 1 تا 12 باشد");
                        return Task.CompletedTask;
                    }
                    
                    // بررسی تعداد روزهای ماه
                    int daysInMonth = persianCalendar.GetDaysInMonth(year, month);
                    if (day < 1 || day > daysInMonth)
                    {
                        bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"ماه {month} سال {year} فقط {daysInMonth} روز دارد");
                        return Task.CompletedTask;
                    }
                    
                    var gregorianDate = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
                    
                    if (bindingContext.ModelType == typeof(DateTime) || bindingContext.ModelType == typeof(DateTime?))
                    {
                        bindingContext.Result = ModelBindingResult.Success(gregorianDate);
                    }
                    else if (bindingContext.ModelType == typeof(DateOnly) || bindingContext.ModelType == typeof(DateOnly?))
                    {
                        bindingContext.Result = ModelBindingResult.Success(DateOnly.FromDateTime(gregorianDate));
                    }
                }
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"فرمت تاریخ نامعتبر است: {value} - {ex.Message}");
            }

            return Task.CompletedTask;
        }
    }

    public class PersianDateModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(DateTime) ||
                context.Metadata.ModelType == typeof(DateTime?) ||
                context.Metadata.ModelType == typeof(DateOnly) ||
                context.Metadata.ModelType == typeof(DateOnly?))
            {
                return new PersianDateModelBinder();
            }

            return null;
        }
    }
}
