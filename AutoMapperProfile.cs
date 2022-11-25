using AutoMapper;
using Task_Manager_Api.DTOs.Person;
using Task_Manager_Api.DTOs.TaskItem;
using Task_Manager_Api.Models;

namespace Task_Manager_Api
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TaskItem, GetTaskItemDto>();
            CreateMap<AddTaskItemDto, TaskItem>();
            CreateMap<Person, GetPersonDto>();
            CreateMap<AddPersonDto, Person>();
        }
    }
}
