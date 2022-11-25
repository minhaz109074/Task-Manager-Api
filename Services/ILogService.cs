using Task_Manager_Api.Models;

namespace Task_Manager_Api.Services
{
    public interface ILogService
    {
        public Task CreateExceptionLog(Exception ex);
        public Task CreateRequestResponseLog(Dictionary<string, dynamic> requestLogs, Dictionary<string, dynamic> responseLogs);

    }
}
