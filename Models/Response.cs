namespace Task_Manager_Api.Models
{
    public class Response<T>
    {
        
        public T? Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        
    }
}
