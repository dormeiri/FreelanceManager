using DataAccess.Entities;
using System;

namespace DataAccess.Models
{
    public class Bill : Record
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string WorkTimeRangesFilePath { get; set; }
    }
}
