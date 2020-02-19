using DataAccess;
using DataAccess.Models;

namespace FreelanceManager.Commands.Projects
{
    public class ProjectAddCommand : ICommand
    {
        public string Name => "prjadd";

        public void Run(Context ctx, string[] args)
        {
            var entity = new WorkProject() { Name = args[0] };

            ctx.WorkProjects.Add(entity);

            ctx.Save();
        }
    }
}
