using DataAccess;
using DataAccess.Models;

namespace FreelanceManager.Commands.Tasks
{
    public class TaskAddCommand : ICommand
    {
        public string Name => "tskadd";

        public void Run(Context ctx, string[] args)
        {
            var entity = new WorkTask()
            {
                Name = args[0],
                WorkProjectId = int.Parse(args[1])
            };

            ctx.WorkTasks.Add(entity);

            ctx.Save();
        }
    }
}
