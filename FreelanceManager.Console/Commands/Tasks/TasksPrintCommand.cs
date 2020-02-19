using DataAccess;
using System;

namespace FreelanceManager.Commands.Tasks
{
    public class TasksPrintCommand : ICommand
    {
        public string Name => "tsks";

        public void Run(Context ctx, string[] args)
        {
            foreach (var t in ctx.WorkTasks.AsEnumerable())
            {
                var project = ctx.WorkProjects.Find(t.WorkProjectId);

                Console.WriteLine($"{t.Id}, {t.Name}, {project.Name}");
            }
        }
    }
}
