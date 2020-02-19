using DataAccess;

namespace FreelanceManager.Commands
{
    public class ExitCommand : ICommand
    {
        public string Name => "exit";

        public void Run(Context ctx, string[] args) { }
    }
}
