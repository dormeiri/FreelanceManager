using DataAccess;
using System;

namespace FreelanceManager.Commands.Tasks
{
    public class BillsPrintCommand : ICommand
    {
        public string Name => "bils";

        public void Run(Context ctx, string[] args)
        {
            foreach (var b in ctx.Bills.AsEnumerable())
            {
                Console.WriteLine($"{b.Id}, {b.Start}, {b.End}, {b.WorkTimeRangesFilePath}");
            }
        }
    }
}
