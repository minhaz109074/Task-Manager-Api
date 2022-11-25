using System.ComponentModel.DataAnnotations;

namespace Task_Manager_Api.DTOs.TaskItem
{
    public class AddTaskItemDto
    {
        public string Title { get; set; } = "Bring dalbaji with parata";

        [Required(ErrorMessage = "Task Requested by field can't be blank")]
        public string TaskRequestedBy { get; set; }
        public string? TaskAssignedTo { get; set; }

    }
}
