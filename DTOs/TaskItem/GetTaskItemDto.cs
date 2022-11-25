using Task_Manager_Api.Models;

namespace Task_Manager_Api.DTOs.TaskItem
{
    public class GetTaskItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string TaskRequestedBy { get; set; }
        public string? TaskAssignedTo { get; set; }
        public bool IsCompleted { get; set; }

    }
}
