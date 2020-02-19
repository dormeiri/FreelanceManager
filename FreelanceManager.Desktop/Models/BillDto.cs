using DataAccess.Models;
using System;

namespace FreelanceManager.Desktop.Models
{
    public class BillDto
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string WorkTimeRangesFilePath { get; set; }

        public BillDto() { }

        public BillDto(Bill bill)
        {
            Id = bill.Id;
            Start = bill.Start;
            End = bill.End;
        }

        public static explicit operator Bill(BillDto dto)
        {
            return new Bill()
            {
                Start = dto.Start,
                End = dto.End,
                WorkTimeRangesFilePath = dto.WorkTimeRangesFilePath
            };
        }

        public override string ToString()
        {
            return $"{Id}, {Start}, {End}";
        }
    }
}
