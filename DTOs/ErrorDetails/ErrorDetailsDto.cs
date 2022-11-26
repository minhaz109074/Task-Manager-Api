namespace Task_Manager_Api.DTOs.ErrorDetails
{
    public class ErrorDetailsDto
    {
        public int? StatusCode  { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string? ErrorMessage { get; set; }
    }
}
