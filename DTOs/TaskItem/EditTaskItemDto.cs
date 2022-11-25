using System.ComponentModel.DataAnnotations;

namespace Task_Manager_Api.DTOs.TaskItem
{
    public class EditTaskItemDto
    {
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
