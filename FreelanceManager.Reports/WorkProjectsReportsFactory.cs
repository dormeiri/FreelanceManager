using DataAccess;
using DataAccess.Models;
using FreelanceManager.Reports.Entities;
using Statistics;
using System.Collections.Generic;

namespace FreelanceManager.Reports
{
    public class WorkProjectsReportsFactory : ReportsFactory<WorkProject, WorkProjectsReport, WorkProjectsReport.Record>
    {
        protected override IEnumerable<WorkProject> Source => _ctx.WorkProjects.AsEnumerable();

        public WorkProjectsReportsFactory(Context ctx) : base(ctx) { }

        protected override IEnumerable<WorkProjectsReport.Record> Mapper()
        {
            var totalHours = new StatisticsClient(_ctx).GetTotalHoursByProject();

            foreach (var record in Source)
            {
                yield return new WorkProjectsReport.Record()
                {
                    Project = record.Name,
                    Hours = totalHours[record.Id]
                };
            }
        }
    }
}
