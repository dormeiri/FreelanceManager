using DataAccess;
using DataAccess.Models;
using FreelanceManager.Reports.Entities;
using System.Collections.Generic;

namespace FreelanceManager.Reports
{
    public class BillFullReportsFactory : ReportsFactory<WorkTimeRange, BillFullReport, BillFullReport.Record>
    {
        protected override IEnumerable<WorkTimeRange> Source => _ctx.WorkTimeRanges.AsEnumerable();

        public BillFullReportsFactory(Context ctx) : base(ctx) { }

        protected override IEnumerable<BillFullReport.Record> Mapper()
        {
            foreach (var record in Source)
            {
                var task = _ctx.WorkTasks.Find(record.WorkTaskId);
                var project = _ctx.WorkProjects.Find(task.WorkProjectId);

                yield return new BillFullReport.Record()
                {
                    Project = project.Name,
                    Task = task.Name,
                    Start = record.Start,
                    End = record.End,
                    Hours = (record.End - record.Start).TotalHours
                };
            }
        }
    }
}
