using DataAccess;

namespace FreelanceManager.Commands.Tasks
{
    public class TaskRemoveCommand : ICommand
    {
        public string Name => "tskrem";

        public void Run(Context ctx, string[] args)
        {
            ctx.WorkTasks.Remove(int.Parse(args[0]));

            ctx.Save();
        }
    }
}
