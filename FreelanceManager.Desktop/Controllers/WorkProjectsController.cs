using DataAccess;
using DataAccess.Models;
using FreelanceManager.Desktop.Models;
using System.Collections.Generic;
using System.Linq;

namespace FreelanceManager.Desktop.Controllers
{
    public class WorkProjectsController : Controller
    {
        public event ListChangedEventHandler ListChanged;

        public WorkProjectsController(Context ctx) : base(ctx) { }

        public ICollection<WorkProjectDto> Get()
        {
            return _ctx.WorkProjects.AsEnumerable().Select(x => new WorkProjectDto(x)).ToList();
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
    }
}
