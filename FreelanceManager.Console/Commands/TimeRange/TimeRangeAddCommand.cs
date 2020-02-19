using DataAccess;
using DataAccess.Models;
using System;
using System.Globalization;

namespace FreelanceManager.Commands.Tasks
{
    public class TimeRangeAddCommand : ICommand
    {
        public string Name => "tmrngadd";

        public void Run(Context ctx, string[] args)
        {
            // TODO: Duplicate
            var start = DateTime.Parse(args[0], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            var end = DateTime.Parse(args[1], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

            var entity = new WorkTimeRange()
            {
                Start = start,
                End = end,
                WorkTaskId = int.Parse(args[2])
            };

            ctx.WorkTimeRanges.Add(entity);

            ctx.Save();
        }
    }
}
