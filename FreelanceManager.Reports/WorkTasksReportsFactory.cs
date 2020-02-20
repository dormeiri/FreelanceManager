using DataAccess;
using DataAccess.Models;
using FreelanceManager.Reports.Entities;
using Statistics;
using System.Collections.Generic;
using System.Linq;

namespace FreelanceManager.Reports
{
    public class WorkTasksReportsFactory : ReportsFactory<WorkTask, WorkTasksReport, WorkTasksReport.Record>
    {
        private readonly int _projectId;

        protected override IEnumerable<WorkTask> Source
        {
            get
            {
                return _ctx.WorkTasks
                    .AsEnumerable()
                    .Where(p => p.WorkProjectId == _projectId);
            }
        }

        public WorkTasksReportsFactory(Context ctx, int projectId) : base(ctx)
        {
            _projectId = projectId;
        }

        public override WorkTasksReport Produce()
        {
            var result = base.Produce();
            result.Project = _ctx.WorkProjects.Find(_projectId).Name;

            return result;
        }

        protected override IEnumerable<WorkTasksReport.Record> Mapper()
        {
            var totalHours = new StatisticsClient(_ctx).GetTotalHoursByTasks();

            foreach (var record in Source)
            {
                yield return new WorkTasksReport.Record()
                {
                    Task = record.Name,
                    Hours = totalHours[record.Id]
                };
            }
        }
    }
}
