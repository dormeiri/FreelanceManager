using DataAccess;
using DataAccess.Models;
using FreelanceManager.Desktop.Models;
using FreelanceManager.Desktop.View.WorkProjects;
using FreelanceManager.Desktop.View.WorkTasks;
using FreelanceManager.Reports;
using Statistics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FreelanceManager.Desktop.Controllers
{
    public class WorkProjectsController : Controller, IDataSetController
    {
        public event ListChangedEventHandler ListChanged;
        public event ListChangedEventHandler RelatedListChanged;

        public WorkProjectsController(Context ctx) : base(ctx) { }

        public WorkTasksView GetWorkTasksView(int id)
        {
            var controller = new WorkTasksController(_ctx, id);

            controller.BlazeAdded += TriggerBlazeAddedEvent;
            controller.RemoveDialogRequested += (s, o, n) => ShowRemoveDialog(s, o, n);
            controller.ListChanged += () => RelatedListChanged?.Invoke();
            controller.RelatedListChanged += () => RelatedListChanged?.Invoke();

            return new WorkTasksView(controller);
        }

        public WorkProjectAddView GetAddView()
        {
            var entity = new WorkProjectDto();

            return new WorkProjectAddView(this, entity);
        }

        public ICollection<WorkProjectDto> Get()
        {
            var totalHours = new StatisticsClient(_ctx).GetTotalHoursByProject();

            return _ctx.WorkProjects.AsEnumerable()
                .Select(x => new WorkProjectDto(x) { TotalHours = totalHours?.GetValueOrDefault(x.Id) ?? 0 })
                .ToList();
        }

        public void Add(WorkProjectDto entity)
        {
            _ctx.WorkProjects.Add((WorkProject)entity);

            ListChanged?.Invoke();

            _ctx.Save();
        }

        public void Remove(int id)
        {
            _ctx.WorkProjects.Remove(id);

            ListChanged?.Invoke();

            _ctx.Save();
        }

        public double GetTotalHours()
        {
            return new StatisticsClient(_ctx).GetTotalHours();
        }

        public void ExportReport()
        {
            var report = new WorkProjectsReportsFactory(_ctx).Produce();
            report.ToCsv(_ctx.Directory);

            Process.Start(new ProcessStartInfo(_ctx.Directory) { UseShellExecute = true });
        }

        public void UpdateStatistics(IEnumerable<WorkProjectDto> source)
        {
            var totalHours = new StatisticsClient(_ctx).GetTotalHoursByProject();

            foreach (var item in source)
            {
                item.TotalHours = totalHours[item.Id];
            }
        }
    }
}
