using DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace Statistics
{
    public class StatisticsClient
    {
        private readonly Context _ctx;

        public StatisticsClient(Context ctx)
        {
            _ctx = ctx;
        }

        public double GetTotalHours()
        {
            return _ctx.WorkTimeRanges?.AsEnumerable().Sum(x => (x.End - x.Start).TotalHours)
                ?? 0;
        }

        public double GetTotalHoursOfProject(int id)
        {
            return _ctx.WorkTimeRanges?.AsEnumerable()
                .Where(x => _ctx.WorkTasks.Find(x.WorkTaskId).WorkProjectId == id)
                .Sum(x => (x.End - x.Start).TotalHours)
                ?? 0;
        }

        public double GetTotalHoursOfTask(int id)
        {
            return _ctx.WorkTimeRanges?.AsEnumerable()
                .Where(x => x.WorkTaskId == id)
                .Sum(x => (x.End - x.Start).TotalHours)
                ?? 0;
        }

        public Dictionary<int, double> GetTotalHoursByProject()
        {
            var groups = _ctx.WorkProjects.AsEnumerable().ToDictionary(x => x.Id, x => 0D);

            if (_ctx.WorkTimeRanges != null)
            {
                foreach (var x in _ctx.WorkTimeRanges.AsEnumerable())
                {
                    var task = _ctx.WorkTasks.Find(x.WorkTaskId);

                    groups[task.WorkProjectId] += (x.End - x.Start).TotalHours;
                }
            }

            return groups;
        }

        public Dictionary<int, double> GetTotalHoursByTasks()
        {
            var groups = _ctx.WorkTasks.AsEnumerable().ToDictionary(x => x.Id, x => 0D);

            if (_ctx.WorkTimeRanges != null)
            {
                foreach (var x in _ctx.WorkTimeRanges.AsEnumerable())
                {
                    groups[x.WorkTaskId] += (x.End - x.Start).TotalHours;
                }
            }

            return groups;
        }
    }
}
