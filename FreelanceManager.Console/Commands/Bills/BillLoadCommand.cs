using DataAccess;

namespace FreelanceManager.Commands.Tasks
{
    public class BillLoadCommand : ICommand
    {
        public string Name => "bilload";

        public void Run(Context ctx, string[] args)
        {
            var b = ctx.Bills.Find(int.Parse(args[0]));

            ctx.LoadBill(b);
        }
    }
}
