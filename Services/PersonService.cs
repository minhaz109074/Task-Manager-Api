using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task_Manager_Api.DTOs.Person;
using Task_Manager_Api.DTOs.TaskItem;
using Task_Manager_Api.Models;
using Task_Manager_Api.Repositories;
using Task_Manager_Api.Exceptions;

namespace Task_Manager_Api.Services
{
    public class PersonService : IPersonService
    {
        
        private readonly IMapper _mapper;
        private readonly IRepository<Person> _repository;
        public PersonService(IMapper mapper, IRepository<Person> repository) 
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Response<GetPersonDto>> AddPerson(AddPersonDto newPerson)
        {
            var response = new Response<GetPersonDto>();

            var duplicatePerson = await _repository.FindByName(newPerson.Name);
            if (duplicatePerson != null) throw new RecordAlreadyExistException($"Person Name-{newPerson.Name} is already exist. Please add an unique name");

            var person = _mapper.Map<Person>(newPerson);

            await _repository.Add(person);
            response.Data = _mapper.Map<GetPersonDto>(person);
            response.Message = "Successfully added to the database";
            return response;
        }

        public async Task<Response<List<GetPersonDto>>> GetAllPerson()
        {
            var response = new Response<List<GetPersonDto>>();
            var DbPersons = await _repository.GetAll();
            response.Data = DbPersons.Select(p => _mapper.Map<GetPersonDto>(p)).OrderBy(r => r.Name).ToList();
            return response;
        }

        public async Task<Response<GetPersonDto>> GetPersonByName(string personName)
        {
            var response = new Response<GetPersonDto>();
            var person = await _repository.FindByName(personName);
            if (person == null)
            {
                response.IsSuccess = false;
                response.Message = $"{personName} doesn't exist in the database.";
            }

            response.Data = _mapper.Map<GetPersonDto>(person);
            

            return response;
        }

        public async Task<Response<GetPersonDto>> UpdatePerson(string personName, UpdatePersonDto updatedPerson)
        {
            var response = new Response<GetPersonDto>();

            var person = await _repository.FindByName(personName);
            if (person == null) throw new NotFoundException($"{personName} doesn't exist in the database.");

            person.Email = updatedPerson.Email;
            person.Designation = updatedPerson.Designation;

            await _repository.Update(person);
            response.Data = _mapper.Map<GetPersonDto>(person);
            return response;
        }

        public async Task<Response<GetPersonDto>> EditPerson(string personName, EditPersonDto editedPerson)
        {
            var response = new Response<GetPersonDto>();

            var person = await _repository.FindByName(personName);
            if (person == null) throw new NotFoundException($"{personName} doesn't exist in the database.");


            if (editedPerson.Designation != null) person.Designation = editedPerson.Designation;

            await _repository.Update(person);
            response.Data = _mapper.Map<GetPersonDto>(person);
            return response;
        }

        public async Task<Response<List<GetPersonDto>>> DeletePerson(string personName)
        {
            var response = new Response<List<GetPersonDto>>();

            var person = await _repository.FindByName(personName);
            if (person == null) throw new NotFoundException($"{personName} doesn't exist");

            await _repository.Delete(person);
            var DbPersons = await _repository.GetAll();
            response.Data = DbPersons.Select(p => _mapper.Map<GetPersonDto>(p)).OrderBy(r => r.Name).ToList();
            response.Message = $"Successfully deleted  {personName} from the database";
            return response;
        }
    }
}
