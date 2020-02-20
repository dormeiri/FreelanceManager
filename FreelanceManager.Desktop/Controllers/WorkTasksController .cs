using DataAccess;
using DataAccess.Models;
using FreelanceManager.Desktop.Models;
using FreelanceManager.Desktop.View.WorkTasks;
using FreelanceManager.Desktop.View.WorkTimeRanges;
using FreelanceManager.Reports;
using Statistics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FreelanceManager.Desktop.Controllers
{
    public class WorkTasksController : Controller
    {
        private readonly int _projectId;

        public event ListChangedEventHandler ListChanged;
        public event ListChangedEventHandler RelatedListChanged;

        public WorkTasksController(Context ctx, int projectId) : base(ctx)
        {
            _projectId = projectId;
        }

        public WorkTimeRangesView GetWorkTimeRangesView(int id)
        {
            var controller = new WorkTimeRangesController(_ctx, id);

            controller.BlazeAdded += TriggerBlazeAddedEvent;
            controller.ListChanged += () => RelatedListChanged?.Invoke();

            return new WorkTimeRangesView(controller);
        }

        public WorkTaskAddView GetAddView()
        {
            var entity = new WorkTaskDto()
            {
                WorkProjectId = _projectId
            };

            return new WorkTaskAddView(this, entity);
        }

        public ICollection<WorkTaskDto> Get()
        {
            var totalHours = new StatisticsClient(_ctx).GetTotalHoursByTasks();

            return _ctx.WorkTasks.AsEnumerable()
                .Where(x => x.WorkProjectId == _projectId)
                .Select(x => new WorkTaskDto(x) { TotalHours = totalHours?.GetValueOrDefault(x.Id) ?? 0 })
                .ToList();
        }

        public void Add(WorkTaskDto entity)
        {
            _ctx.WorkTasks.Add((WorkTask)entity);

            ListChanged?.Invoke();

            _ctx.Save();
        }

        public void Remove(int id)
        {
            _ctx.WorkTasks.Remove(id);

            ListChanged?.Invoke();

            _ctx.Save();
        }

        public double GetTotalHours()
        {
            return new StatisticsClient(_ctx).GetTotalHoursOfProject(_projectId);
        }

        public void ExportReport()
        {
            var report = new WorkTasksReportsFactory(_ctx, _projectId).Produce();
            report.ToCsv(_ctx.Directory);

            Process.Start(new ProcessStartInfo(_ctx.Directory) { UseShellExecute = true });
        }

        public WorkProjectDto GetWorkProjectContext()
        {
            return new WorkProjectDto(_ctx.WorkProjects.Find(_projectId));
        }
    }
}
