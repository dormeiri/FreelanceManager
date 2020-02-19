using DataAccess;
using DataAccess.Models;
using System;
using System.IO;

namespace FreelanceManager.Commands.Tasks
{
    public class BillAddNowCommand : ICommand
    {
        public string Name => "biladdnow";

        public void Run(Context ctx, string[] args)
        {
            var now = DateTime.Now;

            var start = new DateTime(now.Year, now.Month, 1);
            var end = new DateTime(start.Year, start.Month, DateTime.DaysInMonth(start.Year, start.Month));

            var entity = new Bill()
            {
                Start = start,
                End = end,
                WorkTimeRangesFilePath = Path.Combine(ctx.Directory, $"{start:yyyyMMdd}-{end:yyyyMMdd}.csv"),
            };

            ctx.Bills.Add(entity);

            ctx.Save();
        }
    }
}
