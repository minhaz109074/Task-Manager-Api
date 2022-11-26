using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Task_Manager_Api.DTOs.TaskItem;
using Task_Manager_Api.Models;
using Task_Manager_Api.Repositories;
using Task_Manager_Api.Exceptions;

namespace Task_Manager_Api.Services
{
    public class TaskService : ITaskService
    {
   
        private readonly IMapper _mapper;
        private readonly IRepository<TaskItem> _taskRepository;
        private readonly IRepository<Person> _personRepository;
        public TaskService(IMapper mapper, IRepository<TaskItem> taskRepository, IRepository<Person> personRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _personRepository = personRepository;
        }

        
        public async Task<Response<GetTaskItemDto>> AddTask(AddTaskItemDto newTask)
        {
            var response = new Response<GetTaskItemDto>();
            string msg = $" Please send a get request to api/persons to view available people or a post request to add one. " +
                          $"Note: Here person name must have to be unique";

            var taskRequester = await _personRepository.FindByName(newTask.TaskRequestedBy);
            if (taskRequester == null) throw new NotFoundException($"Task Requested by person-{newTask.TaskRequestedBy} doesn't exist in the system." + $"{msg}");
            if (newTask.TaskAssignedTo != null)
            {

                var taskAssignedto = await _personRepository.FindByName(newTask.TaskAssignedTo);
                if (taskAssignedto == null) throw new NotFoundException($"Task Assigned to person-{newTask?.TaskAssignedTo} doesn't exist in the system." + $"{msg}");
            }

            var task = _mapper.Map<TaskItem>(newTask);
            task.IsCompleted = false;

            await _taskRepository.Add(task);
            response.Data = _mapper.Map<GetTaskItemDto>(task);
            response.Message = "Successfully added to the database";
            return response;

        }

        public async Task<Response<List<GetTaskItemDto>>> GetAllTask()
        {
            var response = new Response<List<GetTaskItemDto>>();
            var DbTasks = await _taskRepository.GetAll();
            response.Data = DbTasks.Select(t => _mapper.Map<GetTaskItemDto>(t)).OrderBy(r => r.Id).ToList();
            return response;
        }
        

        public async Task<Response<List<GetTaskItemDto>>> GetIndividualTasks(string personName, string type, [Optional] DateTime? queryDate)
        {
            var response = new Response<List<GetTaskItemDto>>();
            var DbAlltask = await _taskRepository.GetAll();
            IEnumerable<TaskItem> IndividualTasks;

            if(type == "completed-tasks")
            {
                IndividualTasks = DbAlltask.Where(t => t.TaskAssignedTo == personName && t.IsCompleted);
            }
            else {
                if (queryDate == null)
                {
                    IndividualTasks = DbAlltask.Where(t => type == "requested-tasks" ? t.TaskRequestedBy == personName : t.TaskAssignedTo == personName);
                }
                else
                {
                    IndividualTasks =  DbAlltask.Where(t => (type == "requested-tasks" ? t.TaskRequestedBy == personName : t.TaskAssignedTo == personName) &&
                           t.CreatedAt != null && t.CreatedAt!.Value.Date.Year == queryDate?.Year
                            && t.CreatedAt.Value.Date.Month == queryDate?.Month
                            && t.CreatedAt.Value.Date.Day == queryDate?.Day);
                }
            }

            
            response.Data = IndividualTasks.Select(t => _mapper.Map<GetTaskItemDto>(t)).OrderBy(p => p.Id).ToList();

            if (response.Data.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = $"No {type.Split("-")[0]} task found";
            }
            
            return response;
        }

        public async Task<Response<List<GetTaskItemDto>>> GetSearchedTasks(string queryWord)
        {
            var response = new Response<List<GetTaskItemDto>>();
            queryWord = queryWord.ToLower();
            var SearchedTaks =  await _taskRepository.FindByCondition(t => t.Id.ToString().Contains(queryWord)
                                || t.Title.ToLower().Contains(queryWord)
                                || t.TaskRequestedBy.ToLower().Contains(queryWord) 
                                || (t.TaskAssignedTo != null && t.TaskAssignedTo!.ToLower().Contains(queryWord))
                                || (t.CreatedAt != null && t.CreatedAt!.ToString()!.ToLower().Contains(queryWord))
                                || (t.UpdatedAt != null && t.UpdatedAt!.ToString()!.ToLower().Contains(queryWord))
                                || t.IsCompleted.ToString().ToLower().Contains(queryWord));
            response.Data = SearchedTaks.Select(t => _mapper.Map<GetTaskItemDto>(t)).OrderBy(t => t.Id).ToList();
            if (response.Data.Count == 0)
            {
                response.IsSuccess = false;
                response.Message = "No task found";
            }
            return response;

        }

        public async Task<Response<GetTaskItemDto>> UpdateTask(int id, UpdateTaskItemDto updatedTask)
        {
            var response = new Response<GetTaskItemDto>();

            var task = await _taskRepository.FindById(id);

            if (task == null) throw new NotFoundException($"Task {id} doesn't exist");


            var taskRequester = await _personRepository.FindByName(updatedTask.TaskRequestedBy);
            if (taskRequester == null) throw new NotFoundException("Task Requested by person doesn't exist in the system");
            var taskAssignedto = await _personRepository.FindByName(updatedTask.TaskAssignedTo);
            if (taskAssignedto == null) throw new NotFoundException("Task Assigned to person doesn't exist in the system");


            task.Title = updatedTask.Title;
            task.TaskRequestedBy = updatedTask.TaskRequestedBy;
            task.TaskAssignedTo = updatedTask.TaskAssignedTo;
            task.IsCompleted = updatedTask.IsCompleted;

            await _taskRepository.Update(task);
            response.Data = _mapper.Map<GetTaskItemDto>(task);

            return response;
        }

        public async Task<Response<GetTaskItemDto>> EditTask(int id, EditTaskItemDto editedTask)
        {
            var response = new Response<GetTaskItemDto>();

            var task = await _taskRepository.FindById(id);

            if (task == null) throw new NotFoundException($"Task {id} doesn't exist");
            if (editedTask.Title != null) { task.Title = editedTask.Title; }
            if (editedTask.IsCompleted.ToString() == "True" || editedTask.IsCompleted.ToString() == "False")
            {
                task.IsCompleted = editedTask.IsCompleted;
            }

            await _taskRepository.Update(task);
            response.Data = _mapper.Map<GetTaskItemDto>(task);

            return response;

        }

        public async Task<Response<List<GetTaskItemDto>>> DeleteTask(int id)
        {
            var response = new Response<List<GetTaskItemDto>>();

            var task = await _taskRepository.FindById(id);

            if (task == null) throw new NotFoundException($"Task {id} doesn't exist");

            await _taskRepository.Delete(task);

            var DbTasks = await _taskRepository.GetAll();
            response.Data = DbTasks.Select(t => _mapper.Map<GetTaskItemDto>(t)).OrderBy(r => r.Id).ToList();
            response.Message = $"Successfully deleted task-id {id} from the database";

            return response;
        }

        
    }
}
