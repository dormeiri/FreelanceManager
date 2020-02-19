using DataAccess;
using System;

namespace FreelanceManager.Commands.Tasks
{
    public class TimeRangesPrintCommand : ICommand
    {
        public string Name => "tmrngs";

        public void Run(Context ctx, string[] args)
        {
            foreach (var tmrng in ctx.WorkTimeRanges.AsEnumerable())
            {
                var task = ctx.WorkTasks.Find(tmrng.WorkTaskId);
                var project = ctx.WorkProjects.Find(task.WorkProjectId);

                Console.WriteLine($"{tmrng.Id}, {tmrng.Start}, {tmrng.End}, {task.Name}, {project.Name}");
            }
        }
    }
}
