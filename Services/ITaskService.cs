using System.Runtime.InteropServices;
using Task_Manager_Api.DTOs.TaskItem;
using Task_Manager_Api.Models;

namespace Task_Manager_Api.Services
{
    public interface ITaskService
    {
        public Task<Response<GetTaskItemDto>> AddTask(AddTaskItemDto newTask);
        public Task<Response<List<GetTaskItemDto>>> GetAllTask();
        public Task<Response<List<GetTaskItemDto>>> GetIndividualTasks(string personName, string type, [Optional] DateTime? queryDate);
        public Task<Response<List<GetTaskItemDto>>> GetSearchedTasks(string queryWord);
        public Task<Response<GetTaskItemDto>> UpdateTask(int id, UpdateTaskItemDto updatedTask);
        public Task<Response<GetTaskItemDto>> EditTask(int id,  EditTaskItemDto editedTask);
        public Task<Response<List<GetTaskItemDto>>> DeleteTask(int id);
        


    }
}
