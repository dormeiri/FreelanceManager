using DataAccess;
using Statistics;
using System;

namespace FreelanceManager.Commands.Statistics
{
    public class TotalHoursByTaskCommand : ICommand
    {
        public string Name => "hrsbytsk";

        public void Run(Context ctx, string[] args)
        {
            var client = new StatisticsClient(ctx);
            foreach (var x in client.GetTotalHoursByTasks())
            {
                var task = ctx.WorkTasks.Find(x.Key);
                var project = ctx.WorkProjects.Find(task.WorkProjectId);

                Console.WriteLine($"{project.Name} {task.Name} : {x.Value.ToString("0.0")}");
            }
        }
    }
}
