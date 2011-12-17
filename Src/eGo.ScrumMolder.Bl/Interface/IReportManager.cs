using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace eGo.ScrumMolder.Bl.Interface
{
    public interface IReportManager
    {
        Stream GetReport(DateTime dateFrom, DateTime dateTo);
    }
}
