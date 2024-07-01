using Microsoft.AspNetCore.Http;

namespace Services.CrewsAdmin
{
    public class PersonDTO
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public IFormFile? PersonImage { get; set; }
    }
}
