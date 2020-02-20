using DataAccess;
using DataAccess.Models;
using FreelanceManager.Desktop.Models;
using FreelanceManager.Desktop.View.WorkTimeRanges;
using Statistics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreelanceManager.Desktop.Controllers
{
    public class WorkTimeRangesController : Controller
    {
        public int TaskId { get; private set; }

        public event ListChangedEventHandler ListChanged;

        public WorkTimeRangesController(Context ctx, int taskId) : base(ctx)
        {
            TaskId = taskId;
        }

        public WorkTimeRangeAddView GetAddView()
        {
            var now = DateTime.Now;
            now = now.Date.AddHours(now.Hour);

            var entity = new WorkTimeRangeDto()
            {
                Start = now.AddHours(-1),
                End = now,
                WorkTaskId = TaskId
            };

            return new WorkTimeRangeAddView(this, entity);
        }

        public ICollection<WorkTimeRangeDto> Get()
        {
            return _ctx.WorkTimeRanges?.AsEnumerable()
                .Where(x => x.WorkTaskId == TaskId)
                .Select(x => new WorkTimeRangeDto(x))
                .ToList();
        }

        public void Add(WorkTimeRangeDto entity)
        {
            _ctx.WorkTimeRanges?.Add((WorkTimeRange)entity);

            ListChanged?.Invoke();

            _ctx.Save();
        }

        public void Remove(int id)
        {
            _ctx.WorkTimeRanges?.Remove(id);

            ListChanged?.Invoke();

            _ctx.Save();
        }

        public double GetTotalHours()
        {
            return new StatisticsClient(_ctx).GetTotalHoursOfTask(TaskId);
        }
    }
}
