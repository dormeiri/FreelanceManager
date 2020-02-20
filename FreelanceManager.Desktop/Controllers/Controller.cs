using DataAccess;

namespace FreelanceManager.Desktop.Controllers
{
    public abstract class Controller
    {
        public event BlazeAddedEventHandler BlazeAdded;

        public delegate void BlazeAddedEventHandler();
        public delegate void ListChangedEventHandler();

        protected readonly Context _ctx;

        public Controller(Context ctx)
        {
            _ctx = ctx;
        }

        public void TriggerBlazeAddedEvent()
        {
            BlazeAdded?.Invoke();
        }
    }
}
