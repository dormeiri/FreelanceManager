using DataAccess;
using System;

namespace FreelanceManager.Commands.Tasks
{
    public class BillLoadedPrintCommand : ICommand
    {
        public string Name => "billoaded";

        public void Run(Context ctx, string[] args)
        {
            var b = ctx.Bill;

            Console.WriteLine($"{b.Id}, {b.Start}, {b.End}, {b.WorkTimeRangesFilePath}");
        }
    }
}
