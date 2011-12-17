using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Aspose.Cells;
using log4net;
using eGo.ScrumMolder.Data;
using eGo.ScrumMolder.Bl.Interface;
using System.IO;

namespace eGo.ScrumMolder.Bl
{
    public class ScrumDailyReport
    {
        public DateTime CreateDate { get; set; }
        public string Client { get; set; }
        public string Project { get; set; }
        public string Member { get; set; }
        public TimeSpan SpentTime { get; set; }
    }

    public class ReportManager : IReportManager
    {
        private readonly ILog _logger;
        private readonly ScrumMolderContext _context;

        public ReportManager()
        {
            _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _context = new ScrumMolderContext();
        }

        public Stream GetReport(DateTime dateFrom, DateTime dateTo)
        {
            return GetScrumDailyReport(dateFrom, dateTo);
        }

        public Stream GetScrumDailyReport(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var wb = new Workbook();
                var templatePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                templatePath = templatePath.Remove(0, 6);

                PrepareExcel(ref wb, templatePath);
                var fileName = "";

                var dt = GetData(dateFrom, dateTo);
                if (dt == null || dt.Count == 0)
                    return null;

                FillSheet(wb, dt, dateFrom, dateTo);

                wb.Worksheets.RemoveAt(0);
                wb.Worksheets.ActiveSheetIndex = 0;
                
                
                return wb.SaveToStream();
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Exception: {0}", ex);
                return null;
            }
        }

        protected List<ScrumDailyReport> GetData(DateTime dateFrom, DateTime dateTo)
        {
            var joinQuery =
                  from dailyProjectScrum in _context.DailyProjectScrums
                  join project in _context.Projects on dailyProjectScrum.ProjectId equals project.Id
                  join client in _context.Clients on project.ClientId equals client.Id
                  join dailyScrum in _context.DailyScrums on dailyProjectScrum.DailyScrumId equals dailyScrum.Id
                  join user in _context.Users on dailyScrum.User.Id equals user.Id
                  select new ScrumDailyReport { Client = client.Name, Project = project.Name, Member = user.UserName, CreateDate = dailyScrum.CreateDate, SpentTime = dailyProjectScrum.SpentTime };


            var _dateTo = dateTo.AddDays(1);
            var dt = joinQuery.Where(item => item.CreateDate >= dateFrom && item.CreateDate <= _dateTo)
                .OrderBy(item => item.Client)
                .ThenBy(item => item.Project)
                 .ThenBy(item => item.Member)
                .ThenBy(item => item.CreateDate)
                .ToList();
            return dt;
        }

        /// <summary>
        /// Create workbook for excel
        /// </summary>
        protected void PrepareExcel(ref Workbook wb, string templatePath)
        {

            var templateName = Path.Combine(templatePath, "ReportTemplates\\ScrumMoulderTemplate.xls");
            wb = InitialAsposeCells(templatePath);
            wb.Open(templateName);
        }

        private Workbook InitialAsposeCells(string templatePath)
        {
            License license = new License();
            license.SetLicense(templatePath + "\\Aspose.Cells.lic");
            Workbook wb = new Workbook();

            return wb;
        }

        /// <summary>
        /// Fill excel sheet from list of data
        /// </summary>
        /// <param name="wb">Excel workbook</param>
        /// <param name="dt">List of data</param>
        /// <param name="dateFrom">Begin of period for report</param>
        /// <param name="dateTo">End of period for report</param>
        protected void FillSheet(Workbook wb, List<ScrumDailyReport> dt, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var tempSheet = wb.Worksheets[0];
                var countSheet = 1;

                var listWeeks = Week.WeeksForReport(new Week(dateTo, dateFrom));
                foreach (var week in listWeeks)
                {
                    wb.Worksheets.AddCopy(0);
                    FillSheetForWeek(wb, countSheet, dt.Where(item => item.CreateDate >= week.DateFrom && item.CreateDate <= week.DateTo).ToList(), week.DateFrom, week.DateTo);
                    countSheet++;
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Exception: {0}", ex);
            }
        }


        /// <summary>
        /// Fill excel sheet from List<DataRow>
        /// </summary>
        /// <param name="wb">Excel workbook</param>
        /// <param name="curSheet">Current sheet for filling</param>
        /// <param name="dt">List of ScrumDailyReport with info for current week</param>
        /// <param name="dateFrom">begin of current week</param>
        /// <param name="dateTo">end of current week</param>
        protected void FillSheetForWeek(Workbook wb, int curSheet, List<ScrumDailyReport> dt, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var tempSheet = wb.Worksheets[0];
                var sheet = wb.Worksheets[curSheet];
                sheet.Name = dateFrom.ToString("yyyy.MM.dd") + "-" + dateTo.ToString("MM.dd");
                var date = dateFrom;
                var dataForSumCellForCurProject = new List<int>();
                var dataForSumCountMemberForCurProject = new List<int>();
                var curDate = new DateTime();
                for (var j = 0; j < 7; j++)
                {
                    sheet.Cells[0, 3 + j].PutValue(date.ToString("M/d/yyyy"));
                    date = date.AddDays(1);
                }

                if (dt != null && dt.Count != 0)
                {
                    var curClient = "";
                    var curProject = "";
                    var curMember = "";
                    var curSpentTime = new TimeSpan();
                    var cellForCurClient = 0;
                    var countMemberForCurProject = 0;
                    var cellForCurProject = 0;
                    var curRow = 0;
                    foreach (ScrumDailyReport item in dt)
                    {

                        var cellForData = (int)Convert.ToDateTime(item.CreateDate).DayOfWeek + 1;
                        if (cellForData == 7)
                            cellForData = 0;

                        if (curClient != item.Client)
                        {
                            if (curClient != "")
                            {
                                sheet.Cells[cellForCurClient, 11].Formula = "=SUM(L" + (cellForCurClient+2) + ":L" +(curRow+4) + ")";//Sum spent time for this client cell: L...
                                sheet.Cells.CopyRow(tempSheet.Cells, 4, 4 + curRow);
                                
                                curRow += 4;
                            }
                            sheet.Cells.CopyRow(tempSheet.Cells, 1, 1 + curRow);
                            cellForCurClient = 1 + curRow;
                            curClient = item.Client;
                            sheet.Cells[curRow + 1, 0].PutValue(curClient);
                            curProject = "";
                        }
                        if (curProject != item.Project)
                        {
                            if (curProject != "")
                            {

                                curRow += 2;
                            }
                            sheet.Cells.CopyRow(tempSheet.Cells, 2, 2 + curRow);
                            cellForCurProject = 2 + curRow;
                            curProject = item.Project;
                            sheet.Cells[curRow + 2, 1].PutValue(curProject);
                            
                            curMember = "";
                            dataForSumCellForCurProject.Add(cellForCurProject);
                            dataForSumCountMemberForCurProject.Add(countMemberForCurProject);

                            countMemberForCurProject = 0;
                        }

                        if (curMember != item.Member)
                        {
                            if (curMember != "")
                                curRow++;
                            sheet.Cells.CopyRow(tempSheet.Cells, 3, 3 + curRow);
                            curMember = item.Member;
                            sheet.Cells[curRow + 3, 2].PutValue(curMember);
                            curSpentTime = item.SpentTime;
                            sheet.Cells[curRow + 3, 3 + cellForData].PutValue(curSpentTime.TotalHours);
                            curDate = item.CreateDate.Date;
                            countMemberForCurProject++;
                        }
                        else
                        {
                            var spentTime = item.SpentTime;
                            if (curDate != item.CreateDate.Date)
                                curSpentTime = new TimeSpan();
                            curDate = item.CreateDate.Date;
                            
                            curSpentTime += spentTime;
                            sheet.Cells[curRow + 3, 3 + cellForData].PutValue(curSpentTime.TotalHours);//(curSpentTime);
                        }
                    }
                    dataForSumCountMemberForCurProject.Add(countMemberForCurProject);
                    sheet.Cells[cellForCurClient, 11].Formula = "=SUM(L" + (cellForCurClient + 2) + ":L" + (curRow + 4) + ")";//Sum spent time for this client cell: L...

                    sheet.Cells.CopyRow(tempSheet.Cells, 4, 4 + curRow);
                    sheet.Cells.CopyRow(tempSheet.Cells, 5, 5 + curRow);
                    ///formula for sum spent time by project 
                    for (var i = 0; i < dataForSumCellForCurProject.Count; i++)
                    {
                        cellForCurProject = dataForSumCellForCurProject[i];
                        countMemberForCurProject = dataForSumCountMemberForCurProject[i + 1];

                        for (var curProjDay = 0; curProjDay < 7; curProjDay++)
                        {
                            var simbolForCurCell = ConvertColumnToChar(4 + curProjDay);
                            var numberForCurCell = cellForCurProject + 2;
                            var numberEndForCurCell = cellForCurProject + 1 + countMemberForCurProject;

                            sheet.Cells[cellForCurProject, 3 + curProjDay].Formula = "=SUM(" + simbolForCurCell + numberForCurCell.ToString() + ":" + simbolForCurCell + "" + numberEndForCurCell.ToString() + ")";//Sum spent time for this project 
                        }
                    }
                    ///formula for sum spent time by day
                    for (var curProjDay = 0; curProjDay < 7; curProjDay++)
                    {
                        var simbolForCurCell = ConvertColumnToChar(4 + curProjDay);
                        var formulaForDay = "";
                        foreach (var curCell in dataForSumCellForCurProject)
                            formulaForDay += simbolForCurCell + (curCell + 1) + "+";
                        sheet.Cells[5 + curRow, 3 + curProjDay].Formula = "=" + formulaForDay;//Sum spent time for this day 
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Exception: {0}", ex);
            }
        }

        private string ConvertColumnToChar(int iCol)
        {
            int iAlpha;
            int iRemainder;
            iAlpha = iCol / 27;
            iRemainder = iCol - (iAlpha * 26);
            string convertToLetter = "";
            if (iAlpha > 0)
            {
                convertToLetter = Convert.ToChar(iAlpha + 64).ToString();
            }
            if (iRemainder > 0)
            {
                convertToLetter = convertToLetter + Convert.ToChar(iRemainder + 64).ToString();
            }
            return convertToLetter;
        }
    }
}
