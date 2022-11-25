using System.Runtime.InteropServices;
using Task_Manager_Api.DTOs.Person;
using Task_Manager_Api.Models;

namespace Task_Manager_Api.Services
{
    public interface IPersonService
    {
        public Task<Response<GetPersonDto>> AddPerson(AddPersonDto newPerson);
        public Task<Response<List<GetPersonDto>>> GetAllPerson();
        public Task<Response<GetPersonDto>> GetPersonByName(string personName);
        public Task<Response<GetPersonDto>> UpdatePerson(string personName, UpdatePersonDto updatedPerson);
        public Task<Response<GetPersonDto>> EditPerson(string personName, EditPersonDto editedPerson);
        public Task<Response<List<GetPersonDto>>> DeletePerson(string personName);
    }
}
