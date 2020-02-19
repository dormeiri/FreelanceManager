using DataAccess.Entities;

namespace DataAccess.Models
{
    public class WorkTask : Record
    {
        public string Name { get; set; }

        public int WorkProjectId { get; set; }
    }
}
