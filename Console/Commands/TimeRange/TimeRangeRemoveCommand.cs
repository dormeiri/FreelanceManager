using DataAccess;

namespace FreelanceManager.Commands.Tasks
{
    public class TimeRangeRemoveCommand : ICommand
    {
        public string Name => "tmrngrem";

        public void Run(Context ctx, string[] args)
        {
            ctx.WorkTimeRanges.Remove(int.Parse(args[0]));

            ctx.Save();
        }
    }
}
