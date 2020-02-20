using DataAccess.Entities;
using DataAccess.Models;
using System.IO;
using System.Linq;

namespace DataAccess
{
    public class Context
    {
        private const string WORK_PROJECTS_FILENAME = "projects.csv";
        private const string WORK_TASKS_FILENAME = "tasks.csv";
        private const string BILLS_FILENAME = "bills.csv";


        public string Directory { get; }


        public DataSet<WorkProject> WorkProjects { get; private set; }
        public DataSet<WorkTask> WorkTasks { get; private set; }
        public DataSet<Bill> Bills { get; private set; }

        public DataSet<WorkTimeRange> WorkTimeRanges { get; private set; }
        public Bill Bill { get; private set; }


        public Context(string directory)
        {
            Directory = directory;

            WorkProjects = new DataSet<WorkProject>(GetPath(WORK_PROJECTS_FILENAME));
            WorkTasks = new DataSet<WorkTask>(GetPath(WORK_TASKS_FILENAME));
            Bills = new DataSet<Bill>(GetPath(BILLS_FILENAME));

            Load();
        }


        public void Save()
        {
            WorkProjects.Save();
            WorkTasks.Save();
            Bills.Save();
            WorkTimeRanges?.Save();

            if (Bill == null || !Bills.IsExist(Bill.Id))
            {
                LoadLastBill();
            }
        }

        private void Load()
        {
            WorkProjects.Load();
            WorkTasks.Load();
            Bills.Load();
            LoadLastBill();
        }

        private void LoadLastBill()
        {
            if (Bills.AsEnumerable().Any())
            {
                var m = Bills.AsEnumerable().Max(x => x.Id);
                var bill = Bills.AsEnumerable().First(x => x.Id == m);

                LoadBill(bill);
            }
            else
            {
                Bill = null;
            }
        }

        public void LoadBill(int id)
        {
            var bill = Bills.Find(id);

            LoadBill(bill);
        }

        public void LoadBill(Bill bill)
        {
            Bill = bill;

            WorkTimeRanges = new DataSet<WorkTimeRange>(Bill.WorkTimeRangesFilePath);
            WorkTimeRanges.Load();
        }

        private string GetPath(string filename)
        {
            return Path.Combine(Directory, filename);
        }
    }
}
