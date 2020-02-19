using DataAccess;
using Statistics;
using System;

namespace FreelanceManager.Commands.Statistics
{
    public class TotalHoursByProjectCommand : ICommand
    {
        public string Name => "hrsbyprj";

        public void Run(Context ctx, string[] args)
        {
            var client = new StatisticsClient(ctx);
            foreach (var x in client.GetTotalHoursByProject())
            {
                var project = ctx.WorkProjects.Find(x.Key);

                Console.WriteLine($"{project.Name} : {x.Value.ToString("0.0")}");
            }
        }
    }
}
