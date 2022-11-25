using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Task_Manager_Api.DTOs.TaskItem;
using Task_Manager_Api.Models;
using Task_Manager_Api.Repositories;
using Task_Manager_Api.Services;

namespace Task_Manager_Api.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private dynamic? response;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<ActionResult<Response<GetTaskItemDto>>> AddTask(AddTaskItemDto newTask)
        {
            if(!ModelState.IsValid) return BadRequest();
            response = await _taskService.AddTask(newTask);
            if (!response.IsSuccess) return BadRequest(response);
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<GetTaskItemDto>>>> GetAllTask()
        {
            
            return  Ok(await _taskService.GetAllTask());
        }
        
        [HttpGet("{name}/assigned-tasks")]
        public async Task<ActionResult<Response<List<GetTaskItemDto>>>> GetIndividualAssignedTasks(string name, DateTime? date)
        {
            response = await _taskService.GetIndividualTasks(name, "assigned-tasks", date);
            if (!response.IsSuccess) return NotFound(response);
            return Ok(response);
        }
        [HttpGet("{name}/requested-tasks")]
        public async Task<ActionResult<Response<List<GetTaskItemDto>>>> GetIndividualRequestedTasks(string name, DateTime? date)
        {
            response = await _taskService.GetIndividualTasks(name, "requested-tasks", date);
            if (!response.IsSuccess) return NotFound(response);
            return Ok(response);
        }

        [HttpGet("{name}/completed-tasks")]
        public async Task<ActionResult<Response<List<GetTaskItemDto>>>> GetIndividualCompletedTasks(string name)
        {
            response = await _taskService.GetIndividualTasks(name, "completed-tasks");
            if (!response.IsSuccess) return NotFound(response);
            return Ok(response);
        }

        [HttpGet("{searchTerm}")]
        public async Task<ActionResult<Response<List<GetTaskItemDto>>>> GetSeachedTasks(string searchTerm)
        {
            response = await _taskService.GetSearchedTasks(searchTerm);
            if (!response.IsSuccess) return NotFound(response);
            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<GetTaskItemDto>>> UpdateTask(int id, UpdateTaskItemDto updatedTask)
        {
            if(!ModelState.IsValid) return BadRequest();
            response = await _taskService.UpdateTask(id, updatedTask);
            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Response<GetTaskItemDto>>> EditTask(int id, EditTaskItemDto editedTask)
        {
            if (!ModelState.IsValid) return BadRequest();
            response = await _taskService.EditTask(id, editedTask);
            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<List<GetTaskItemDto>>>> DeleteTask(int id)
        {
    
            response = await _taskService.DeleteTask(id);
            if (!response.IsSuccess) return NotFound(response);
            return Ok(response);
        }
    }
}
