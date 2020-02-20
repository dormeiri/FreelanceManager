using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DataAccess.Entities
{
    public class DataSet<T> where T : Record
    {
        private readonly string _filePath;

        private int _maxId;

        private Dictionary<int, T> _records;


        public DataSet(string filePath)
        {
            _filePath = filePath;
        }


        public void Add(T record)
        {
            record.Id = ++_maxId;

            _records.Add(record.Id, record);
        }

        public void Remove(int id)
        {
            _records.Remove(id);
        }


        public IEnumerable<T> AsEnumerable()
        {
            return _records.Values;
        }

        public bool IsExist(int id)
        {
            return _records.ContainsKey(id);
        }

        public T Find(int id)
        {
            return _records[id];
        }


        internal void Save()
        {
            var stageFilePath = Path.GetTempFileName();

            using (var stream = File.OpenWrite(stageFilePath))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteHeader<T>();
                csv.NextRecord();
                csv.WriteRecords(_records.Values);
            }

            CopyStage(stageFilePath);
        }

        internal void Load()
        {
            if (!File.Exists(_filePath))
            {
                _records = new Dictionary<int, T>();
            }
            else
            {
                using (var stream = File.OpenRead(_filePath))
                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    _records = csv.GetRecords<T>().ToDictionary(k => k.Id, t => t);
                }
            }

            _maxId = _records?.Count > 0 ? _records.Keys.Max() : 0;
        }


        private void CopyStage(string stageFilePath)
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
            }

            File.Move(stageFilePath, _filePath);
        }
    }
}
