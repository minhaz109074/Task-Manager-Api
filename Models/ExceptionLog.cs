namespace Task_Manager_Api.Models
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Request { get; set; }
        public string? ErrorDescription { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? StackTrace { get; set; }

    }
}
