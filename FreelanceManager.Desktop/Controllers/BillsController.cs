using DataAccess;
using DataAccess.Models;
using FreelanceManager.Desktop.Models;
using FreelanceManager.Desktop.View.Bills;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FreelanceManager.Desktop.Controllers
{
    public class BillsController : Controller
    {
        public delegate void BillLoadedEventHandler();

        public event ListChangedEventHandler ListChanged;
        public event BillLoadedEventHandler BillLoaded;

        public BillsController(Context ctx) : base(ctx) { }

        public BillAddView GetAddView()
        {
            var now = DateTime.Now;

            var entity = new BillDto()
            {
                Start = new DateTime(now.Year, now.Month, 1),
                End = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month))
            };

            return new BillAddView(this, entity);
        }

        public ICollection<BillDto> Get()
        {
            return _ctx.Bills.AsEnumerable()
                .Select(x => new BillDto(x))
                .ToList();
        }

        public void Add(BillDto entity)
        {
            entity.WorkTimeRangesFilePath = Path.Combine(
                _ctx.Directory,
                $"{entity.Start:yyyyMMdd}-{entity.End:yyyyMMdd}.csv"
            );

            _ctx.Bills.Add((Bill)entity);

            ListChanged?.Invoke();

            _ctx.Save();
        }

        public void Remove(int id)
        {
            _ctx.Bills.Remove(id);

            ListChanged?.Invoke();

            _ctx.Save();

            BillLoaded?.Invoke();
        }

        public void LoadBill(int id)
        {
            _ctx.LoadBill(id);

            BillLoaded?.Invoke();
        }
    }
}
