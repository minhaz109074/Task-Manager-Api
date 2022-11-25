using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Manager_Api.Models
{
    public class TaskItem: BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = "Bring dalbaji with parata";
        public string TaskRequestedBy { get; set; }
        public string? TaskAssignedTo { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
