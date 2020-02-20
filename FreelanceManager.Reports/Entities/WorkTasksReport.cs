namespace FreelanceManager.Reports.Entities
{
    public class WorkTasksReport : ReportBase<WorkTasksReport.Record>
    {
        public string Project { get; set; }

        protected override string ReportName => $"{Project.Replace(" ", "-")}_tasks";

        public class Record
        {
            public string Task { get; set; }

            public double Hours { get; set; }
        }
    }
}
