using DataAccess.Models;

namespace FreelanceManager.Desktop.Models
{
    public class WorkTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalHours { get; set; }

        public int WorkProjectId { get; set; }

        public WorkTaskDto() { }
        public WorkTaskDto(WorkTask entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            WorkProjectId = entity.WorkProjectId;
        }

        public static explicit operator WorkTask(WorkTaskDto dto)
        {
            return new WorkTask()
            {
                Id = dto.Id,
                Name = dto.Name,
                WorkProjectId = dto.WorkProjectId
            };
        }

        public override string ToString()
        {
            return $"{Id}, {Name}, {TotalHours}";
        }
    }
}
