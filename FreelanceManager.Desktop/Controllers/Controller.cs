using DataAccess;

namespace FreelanceManager.Desktop.Controllers
{
    public abstract class Controller
    {
        public event BlazeAddedEventHandler BlazeAdded;
        public event RemoveDialogRequestedEventHandler RemoveDialogRequested;

        public delegate void BlazeAddedEventHandler();
        public delegate void ListChangedEventHandler();
        public delegate void RemoveDialogRequestedEventHandler(IDataSetController sender, int id, string name);

        protected readonly Context _ctx;

        public Controller(Context ctx)
        {
            _ctx = ctx;
        }

        public void TriggerBlazeAddedEvent()
        {
            BlazeAdded?.Invoke();
        }

        public void ShowRemoveDialog(IDataSetController sender, int id, string name)
        {
            RemoveDialogRequested?.Invoke(sender, id, name);
        }
    }
}
