using DataAccess;
using Statistics;
using System;

namespace FreelanceManager.Commands.Statistics
{
    public class TotalHoursPrintCommand : ICommand
    {
        public string Name => "hrs";

        public void Run(Context ctx, string[] args)
        {
            var client = new StatisticsClient(ctx);

            Console.WriteLine(client.GetTotalHours().ToString("0.0"));
        }
    }
}
