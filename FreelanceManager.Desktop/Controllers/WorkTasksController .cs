using DataAccess;
using DataAccess.Models;
using FreelanceManager.Desktop.Models;
using System.Collections.Generic;
using System.Linq;

namespace FreelanceManager.Desktop.Controllers
{
    public class WorkTasksController : Controller
    {
        public event ListChangedEventHandler ListChanged;

        public WorkTasksController(Context ctx) : base(ctx) { }

        public ICollection<WorkTaskDto> Get(int projectId)
        {
            return _ctx.WorkTasks.AsEnumerable()
                .Where(x=>x.WorkProjectId == projectId)
                .Select(x => new WorkTaskDto(x))
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
    }
}
