using DataAccess;
using DataAccess.Models;
using System;
using System.Globalization;
using System.IO;

namespace FreelanceManager.Commands.Tasks
{
    public class BillAddCommand : ICommand
    {
        public string Name => "biladd";

        public void Run(Context ctx, string[] args)
        {
            // TODO: Duplicate
            var start = DateTime.Parse(args[0], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            var end = DateTime.Parse(args[1], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

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
