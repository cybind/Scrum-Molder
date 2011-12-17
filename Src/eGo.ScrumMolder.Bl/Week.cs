using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eGo.ScrumMolder.Bl
{
    public class Week
    {
        public DateTime DateFrom { set; get; }
        public DateTime DateTo { set; get; }

        public Week()
        {
        }

        public Week(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public Week(Week date)
        {
            DateFrom = date.DateFrom;
            DateTo = date.DateTo;
        }

        public static void InitWeekBegEnd(ref Week date)
        {
            var daysToSubtract = (int)date.DateFrom.DayOfWeek + 1;

            var dtStart = date.DateFrom.Subtract(TimeSpan.FromDays(daysToSubtract));
            var dtEnd = date.DateFrom.Subtract(TimeSpan.FromDays(1));

            dtStart = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, 0, 0, 0, 0);
            dtEnd = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, 0, 0, 0, 0);

            date.DateFrom = dtStart;
            date.DateTo = dtEnd;
        }

        public static List<Week> WeeksForReport(Week date)
        {
            var res = new List<Week>();
            date.DateFrom = date.DateFrom.AddDays(1);

            var end = date.DateTo;
            while (date.DateFrom >= end)
            {
                Week.InitWeekBegEnd(ref date);
                res.Add(new Week(date));
            }

            res[0].DateTo = res[0].DateFrom.AddDays(6);

            var result = new List<Week>();
            for (var i = res.Count - 1; i >= 0; i--)
            {
                result.Add(res[i]);
            }

            return result;
        }
    }
}
