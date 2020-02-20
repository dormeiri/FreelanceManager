using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FreelanceManager.Reports.Entities
{
    public abstract class ReportBase
    {
        public DateTime GeneratedAt { get; } = DateTime.Now;

        protected abstract string ReportName { get; }

        public DateTime WindowStart { get; set; }

        public DateTime WindowEnd { get; set; }

        public string FileName => $"{ReportName}_{WindowStart:yyyyMMdd}-{WindowEnd:yyyyMMdd}.csv";
    }

    public abstract class ReportBase<T> : ReportBase
    {
        public IEnumerable<T> Records { get; set; }

        public void ToCsv(string directory)
        {
            var path = Path.Combine(directory, FileName);

            using (var stream = File.OpenWrite(path))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteHeader<T>();
                csv.NextRecord();
                csv.WriteRecords(Records);
            }
        }
    }
}
