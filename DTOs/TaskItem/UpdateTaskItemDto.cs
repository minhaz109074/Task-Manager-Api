using System.ComponentModel.DataAnnotations;

namespace Task_Manager_Api.DTOs.TaskItem
{
    public class UpdateTaskItemDto
    {
        [Required(ErrorMessage = "The Title can't be blank")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Task Requested by field can't be blank")]
        public string TaskRequestedBy { get; set; }
        [Required(ErrorMessage = "The TaskAssignedTo field can't be blank")]
        public string TaskAssignedTo { get; set; }
        public bool IsCompleted { get; set; }
    }
}
