using DataAccess.Models;

namespace FreelanceManager.Desktop.Models
{
    public class WorkProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double TotalHours { get; set; }

        public WorkProjectDto() { }

        public WorkProjectDto(WorkProject entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public static explicit operator WorkProject(WorkProjectDto dto)
        {
            return new WorkProject()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public override string ToString()
        {
            return $"{Id}, {Name}, {TotalHours}";
        }
    }
}
