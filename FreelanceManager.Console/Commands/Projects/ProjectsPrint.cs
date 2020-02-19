using DataAccess;
using System;

namespace FreelanceManager.Commands.Projects
{
    public class ProjectsPrint : ICommand
    {
        public string Name => "prjs";

        public void Run(Context ctx, string[] args)
        {
            foreach (var p in ctx.WorkProjects.AsEnumerable())
            {
                Console.WriteLine($"{p.Id}, {p.Name}");
            }
        }
    }
}
