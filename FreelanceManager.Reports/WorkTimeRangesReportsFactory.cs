using DataAccess;
using DataAccess.Models;
using FreelanceManager.Reports.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FreelanceManager.Reports
{
    public class WorkTimeRangesReportsFactory : ReportsFactory<WorkTimeRange, WorkTimeRangesReport, WorkTimeRangesReport.Record>
    {
        private readonly int _taskId;

        protected override IEnumerable<WorkTimeRange> Source
        {
            get
            {
                return _ctx.WorkTimeRanges
                    .AsEnumerable()
                    .Where(p => p.WorkTaskId == _taskId);
            }
        }

        public WorkTimeRangesReportsFactory(Context ctx, int taskId) : base(ctx)
        {
            _taskId = taskId;
        }

        public override WorkTimeRangesReport Produce()
        {
            var result = base.Produce();

            var task = _ctx.WorkTasks.Find(_taskId);
            var project = _ctx.WorkProjects.Find(task.WorkProjectId);

            result.Task = task.Name;
            result.Project = project.Name;

            return result;
        }

        protected override IEnumerable<WorkTimeRangesReport.Record> Mapper()
        {
            foreach (var record in Source)
            {
                yield return new WorkTimeRangesReport.Record()
                {
                    Start = record.Start,
                    End = record.End,
                    Hours = (record.End - record.Start).TotalHours
                };
            }
        }
    }
}
