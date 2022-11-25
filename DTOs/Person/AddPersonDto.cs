using System.ComponentModel.DataAnnotations;

namespace Task_Manager_Api.DTOs.Person
{
    public class AddPersonDto
    {
        [Required(ErrorMessage = "Person Name can't be blank")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Person Email can't be blank")]
        public string Email { get; set; }
        public string? Designation { get; set; }
    }
}
