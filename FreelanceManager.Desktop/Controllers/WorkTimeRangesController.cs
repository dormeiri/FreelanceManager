using DataAccess;
using DataAccess.Models;
using FreelanceManager.Desktop.Models;
using FreelanceManager.Desktop.View.WorkTimeRanges;
using FreelanceManager.Reports;
using Statistics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FreelanceManager.Desktop.Controllers
{
    public class WorkTimeRangesController : Controller, IDataSetController
    {
        private readonly int _taskId;

        public event ListChangedEventHandler ListChanged;

        public WorkTimeRangesController(Context ctx, int taskId) : base(ctx)
        {
            _taskId = taskId;
        }

        public WorkTimeRangeAddView GetAddView()
        {
            var now = DateTime.Now;
            now = now.Date.AddHours(now.Hour);

            var entity = new WorkTimeRangeDto()
            {
                Start = now.AddHours(-1),
                End = now,
                WorkTaskId = _taskId
            };

            return new WorkTimeRangeAddView(this, entity);
        }

        public ICollection<WorkTimeRangeDto> Get()
        {
            return _ctx.WorkTimeRanges?.AsEnumerable()
                .Where(x => x.WorkTaskId == _taskId)
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
            return new StatisticsClient(_ctx).GetTotalHoursOfTask(_taskId);
        }

        public WorkTaskDto GetWorkTaskContext()
        {
            return new WorkTaskDto(_ctx.WorkTasks.Find(_taskId));
        }

        public void ExportReport()
        {
            var report = new WorkTimeRangesReportsFactory(_ctx, _taskId).Produce();
            report.ToCsv(_ctx.Directory);

            Process.Start(new ProcessStartInfo(_ctx.Directory) { UseShellExecute = true });
        }
    }
}
