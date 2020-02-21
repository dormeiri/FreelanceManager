using System;

namespace FreelanceManager.Reports.Entities
{
    public class BillFullReport : ReportBase<BillFullReport.Record>
    {
        protected override string ReportName => "bill";

        public class Record
        {
            public string Project { get; set; }
            public string Task { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public double Hours { get; set; }
        }
    }
}
