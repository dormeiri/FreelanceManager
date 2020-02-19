using DataAccess.Models;
using System;

namespace FreelanceManager.Desktop.Models
{
    public class WorkTimeRangeDto
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int WorkTaskId { get; set; }

        public DateTime StartDate
        {
            get
            {
                return Start.Date;
            }
            set
            {
                var time = Start.TimeOfDay;
                Start = value.Date + time;
            }
        }

        public int StartHour
        {
            get
            {
                return Start.Hour;
            }
            set
            {
                Start = Start.AddHours(value - Start.Hour);
            }
        }

        public DateTime EndDate
        {
            get
            {
                return End.Date;
            }
            set
            {
                var time = End.TimeOfDay;
                End = value.Date + time;
            }
        }

        public int EndHour
        {
            get
            {
                return End.Hour;
            }
            set
            {
                End = End.AddHours(value - End.Hour);
            }
        }

        public WorkTimeRangeDto() { }

        public WorkTimeRangeDto(WorkTimeRange timeRange)
        {
            Id = timeRange.Id;
            Start = timeRange.Start;
            End = timeRange.End;
            WorkTaskId = timeRange.WorkTaskId;
        }

        public static explicit operator WorkTimeRange(WorkTimeRangeDto dto)
        {
            return new WorkTimeRange()
            {
                Start = dto.Start,
                End = dto.End,
                WorkTaskId = dto.WorkTaskId
            };
        }

        public override string ToString()
        {
            return $"{Id}, {Start}, {End}";
        }
    }
}
