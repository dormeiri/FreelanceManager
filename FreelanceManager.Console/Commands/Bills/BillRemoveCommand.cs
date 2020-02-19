using DataAccess;

namespace FreelanceManager.Commands.Tasks
{
    public class BillRemoveCommand : ICommand
    {
        public string Name => "bilrem";

        public void Run(Context ctx, string[] args)
        {
            ctx.Bills.Remove(int.Parse(args[0]));

            ctx.Save();
        }
    }
}
