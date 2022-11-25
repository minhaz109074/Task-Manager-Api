using System.ComponentModel.DataAnnotations;

namespace Task_Manager_Api.Models
{
    public class Person: BaseEntity
    {
        [Key]
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Designation { get; set; }
        public List<TaskItem>? TaskRuqest { get; set; }
        public List<TaskItem>? TaskAssign { get; set; }
    }
}
