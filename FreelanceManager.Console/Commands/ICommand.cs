using DataAccess;

namespace FreelanceManager.Commands
{
    public interface ICommand
    {
        public abstract string Name { get; }

        public abstract void Run(Context ctx, string[] args);
    }
}
