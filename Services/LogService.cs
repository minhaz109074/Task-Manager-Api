using Task_Manager_Api.Models;
using Task_Manager_Api.Repositories;

namespace Task_Manager_Api.Services
{
    public class LogService : ILogService
    {
        
        private readonly IRepository<Log> _logRepository;
        private readonly IRepository<ExceptionLog> _exceptionLogRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LogService(IHttpContextAccessor httpContextAccessor, IRepository<Log> logRepository, IRepository<ExceptionLog> exceptionLogRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _logRepository = logRepository;
            _exceptionLogRepository = exceptionLogRepository;
        }
        public async Task CreateExceptionLog(Exception ex)
        {
            ExceptionLog exceptionLog = new ExceptionLog
            {
                ErrorDescription = ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                Request = $"{_httpContextAccessor.HttpContext?.Request.Method} => {_httpContextAccessor.HttpContext?.Request.Path.Value}",
                Type = ex.GetType().ToString(),
                StackTrace = ex.StackTrace,
                CreateDate = DateTime.Now
            };


            await _exceptionLogRepository.Add(exceptionLog);

        }

        public async Task CreateRequestResponseLog(Dictionary<string, dynamic> requestLogs, Dictionary<string, dynamic> responseLogs)
        {
            Log logger = new Log
            {
                RequestMethod = requestLogs["RequestMethod"],
                RequestPath = requestLogs["RequestPath"],
                RequestArriveTime = requestLogs["RequestArriveTime"],
                RequestLeaveTime = responseLogs["RequestLeaveTime"],
                StatusCode = responseLogs["StatusCode"]
            };
            await _logRepository.Add(logger);
        }
    }
}
