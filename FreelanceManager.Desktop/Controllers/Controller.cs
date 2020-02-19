using DataAccess;

namespace FreelanceManager.Desktop.Controllers
{
    public abstract class Controller
    {
        public delegate void ListChangedEventHandler();

        protected readonly Context _ctx;

        public Controller(Context ctx)
        {
            _ctx = ctx;
        }
    }
}
