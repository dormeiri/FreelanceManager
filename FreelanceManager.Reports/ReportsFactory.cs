using DataAccess;
using FreelanceManager.Reports.Entities;
using System.Collections.Generic;

namespace FreelanceManager.Reports
{
    public abstract class ReportsFactory<S, P, R> where P : ReportBase<R>, new()
    {
        protected readonly Context _ctx;

        public ReportsFactory(Context ctx)
        {
            _ctx = ctx;
        }

        protected abstract IEnumerable<S> Source { get; }

        protected abstract IEnumerable<R> Mapper();

        public virtual P Produce()
        {
            return new P()
            {
                WindowStart = _ctx.Bill.Start,
                WindowEnd = _ctx.Bill.End,
                Records = Mapper()
            };
        }
    }
}
