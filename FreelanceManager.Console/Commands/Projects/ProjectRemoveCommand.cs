using DataAccess;

namespace FreelanceManager.Commands.Projects
{
    public class ProjectRemoveCommand : ICommand
    {
        public string Name => "prjrem";

        public void Run(Context ctx, string[] args)
        {
            ctx.WorkProjects.Remove(int.Parse(args[0]));

            ctx.Save();
        }
    }
}
