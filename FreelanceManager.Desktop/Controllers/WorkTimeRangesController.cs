using DataAccess;
using DataAccess.Models;
using FreelanceManager.Desktop.Models;
using System.Collections.Generic;
using System.Linq;

namespace FreelanceManager.Desktop.Controllers
{
    public class WorkTimeRangesController : Controller
    {
        public event ListChangedEventHandler ListChanged;

        public WorkTimeRangesController(Context ctx) : base(ctx) { }

        public ICollection<WorkTimeRangeDto> Get(int workTaskId)
        {
            return _ctx.WorkTimeRanges?.AsEnumerable()
                .Where(x => x.WorkTaskId == workTaskId)
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
    }
}
