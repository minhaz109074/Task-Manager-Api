using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_Manager_Api.Models;
using Task_Manager_Api.Services;
using Task_Manager_Api.DTOs.Person;

namespace Task_Manager_Api.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        private dynamic? response;
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public async Task<ActionResult<Response<GetPersonDto>>> AddPerson(AddPersonDto newPerson)
        {
            response = await _personService.AddPerson(newPerson);
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<GetPersonDto>>>> GetAllPerson()
        {
            return Ok(await _personService.GetAllPerson());
        }
        [HttpGet("{name}")]
        public async Task<ActionResult<Response<GetPersonDto>>> GetPersonByName(string name)
        {
            response = await _personService.GetPersonByName(name);
            if(!response.IsSuccess) return NotFound(response);
            return Ok(response);
        }
        [HttpPut("{name}")]
        public async Task<ActionResult<Response<GetPersonDto>>> UpdatePerson(string name, UpdatePersonDto updatedPerson)
        {
            
            response = await _personService.UpdatePerson(name, updatedPerson);
            return Ok(response);
        }
        [HttpPatch("{name}")]
        public async Task<ActionResult<Response<GetPersonDto>>> EditPerson(string name, EditPersonDto editedPerson)
        {
           
            response = await _personService.EditPerson(name, editedPerson);
            return Ok(response);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<Response<GetPersonDto>>> DeletePerson(string name)
        {
            
            response = await _personService.DeletePerson(name);
            return Ok(response);
        }


    }
}
