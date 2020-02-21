using System;

namespace FreelanceManager.Reports.Entities
{
    public class WorkTimeRangesReport : ReportBase<WorkTimeRangesReport.Record>
    {
        protected override string ReportName => $"{Project.Replace(" ", "-")}_{Task.Replace(" ", "-")}_time-ranges";
        public string Project { get; set; }
        public string Task { get; set; }

        public class Record
        {

            public DateTime Start { get; set; }

            public DateTime End { get; set; }

            public double Hours { get; set; }
        }
    }
}
