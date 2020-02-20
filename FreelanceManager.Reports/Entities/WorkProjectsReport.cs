namespace FreelanceManager.Reports.Entities
{
    public class WorkProjectsReport : ReportBase<WorkProjectsReport.Record>
    {
        protected override string ReportName => "projects";

        public class Record
        {
            public string Project { get; set; }

            public double Hours { get; set; }
        }
    }
}
