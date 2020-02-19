using DataAccess;
using DataAccess.Models;
using System;

namespace FreelanceManager.Commands.Tasks
{
    public class TimeRangeAddNowPrintCommand : ICommand
    {
        public string Name => "done";

        public void Run(Context ctx, string[] args)
        {
            var now = DateTime.Now;

            var start = now.AddHours(-double.Parse(args[0]));
            var end = now;

            var entity = new WorkTimeRange()
            {
                Start = start,
                End = end,
                WorkTaskId = int.Parse(args[1])
            };

            ctx.WorkTimeRanges.Add(entity);

            ctx.Save();
        }
    }
}
