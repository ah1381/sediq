using System.Globalization;

namespace Sediq.Web.Api.Helpers
{
    public static class PersianDateHelper
    {
        public static string ToPersianDate(this DateTime? date)
        {
            if (!date.HasValue)
                return "";
            
            return date.Value.ToPersianDate();
        }

        public static string ToPersianDate(this DateTime date)
        {
            var pc = new PersianCalendar();
            var minDate = new DateTime(622, 3, 22);
            var maxDate = new DateTime(9999, 12, 31);
            
            if (date < minDate || date > maxDate)
                return date.ToString("yyyy/MM/dd");
                
            return $"{pc.GetYear(date):0000}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}";
        }

        public static string ToPersianDate(this DateOnly? date)
        {
            if (!date.HasValue)
                return "";
            
            return date.Value.ToPersianDate();
        }

        public static string ToPersianDate(this DateOnly date)
        {
            var dt = date.ToDateTime(TimeOnly.MinValue);
            return dt.ToPersianDate();
        }

        public static DateTime? ToGregorianDate(string persianDate)
        {
            if (string.IsNullOrEmpty(persianDate))
                return null;

            try
            {
                // تبدیل اعداد فارسی به انگلیسی
                persianDate = persianDate.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3")
                                       .Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7")
                                       .Replace("۸", "8").Replace("۹", "9");
                
                var pc = new PersianCalendar();
                var parts = persianDate.Split('/');
                
                if (parts.Length == 3)
                {
                    int year = int.Parse(parts[0]);
                    int month = int.Parse(parts[1]);
                    int day = int.Parse(parts[2]);
                    
                    return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
                }
            }
            catch
            {
                // در صورت خطا null برگردان
            }
            
            return null;
        }
    }
}
