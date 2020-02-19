using DataAccess.Entities;
using System;

namespace DataAccess.Models
{
    public class WorkTimeRange : Record
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int WorkTaskId { get; set; }
    }
}
