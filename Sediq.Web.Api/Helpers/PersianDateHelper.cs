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
    }
}
