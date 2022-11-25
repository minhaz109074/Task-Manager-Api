namespace Task_Manager_Api.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string? RequestMethod { get; set; }
        public string? RequestPath { get; set; }
        public DateTime? RequestArriveTime { get; set; }
        public DateTime? RequestLeaveTime { get; set; }
        public int? StatusCode { get; set; }
    }
}
