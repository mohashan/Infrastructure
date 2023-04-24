using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BaseTools
{
    public class DateTools
    {
        public long ConvertToTimestamp(DateTime value)
        {
            long epoch = (((DateTime)value).Ticks - 621355968000000000) / 10000;
            return epoch;
        }

        public DateTime ConvertFromTimeStampToDateTime(long timestamp,bool localTime = false)
        {
            DateTime result = new DateTime((timestamp * 10000) + 621355968000000000);
            return localTime?result.ToLocalTime():result;
        }

        public DateTime ShamsiToMiladi(string ShamsiDate)
        {
            string pureDate = NormalizeShamsiDate(ShamsiDate);
            var seperatedDate = pureDate.Split('/');
            Int32.TryParse(seperatedDate[0], out int Year);
            Int32.TryParse(seperatedDate[1], out int Month);
            Int32.TryParse(seperatedDate[2], out int Day);

            if (Month < 1 ||
                Month > 12 ||
                Day < 1 ||
                (Month <= 6 && Day > 31) ||
                (Month > 6 && Day > 30))
            {
                throw new ArgumentException($"{pureDate} is not a well-formed shamsi date");
            }

            return ShamsiToMiladiBasement(Year, Month, Day);
        }
        private DateTime ShamsiToMiladiBasement(int Year, int Month, int Day)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dt = new DateTime(Year, Month, Day, pc);
            return dt;
        }

        public string MiladiToShamsi(DateTime MiladiDate, bool TimeIncluded = false)
        {
            PersianCalendar pc = new PersianCalendar();
            var result = string.Format("{0}/{1}/{2}",
                pc.GetYear(MiladiDate).ToString(),
                pc.GetMonth(MiladiDate).ToString("0#"),
                pc.GetDayOfMonth(MiladiDate).ToString("0#"));
            if (TimeIncluded)
            {
                result = string.Format("{0} {1}:{2}:{3}",
                    result,
                    MiladiDate.TimeOfDay.Hours.ToString("0#"),
                    MiladiDate.TimeOfDay.Minutes.ToString("0#"),
                    MiladiDate.TimeOfDay.Seconds.ToString("0#"));

            }
            return result;
        }

        private string NormalizeShamsiDate(string ShamsiDate)
        {
            if (string.IsNullOrEmpty(ShamsiDate))
            {
                throw new ArgumentNullException();
            }
            var pureDate = ShamsiDate.Replace('-', '/').Replace(' ', '/').Replace('.', '/').Replace('_', '/').Replace('\\', '/');
            if (!ShamsiDate.Contains("/"))
            {
                if (pureDate.Length != 8)
                {
                    throw new ArgumentException("ShamsiDate must have atleast 8 characters");
                }
                pureDate = pureDate.Insert(4, "/").Insert(7, "/");
            }
            if (pureDate.Count(c => c == '/') != 2)
            {
                throw new ArgumentException($"{pureDate} is not a well-formed shamsi date");
            }

            return pureDate;
        }
    }
}
