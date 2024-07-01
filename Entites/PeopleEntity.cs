using System.ComponentModel.DataAnnotations;

namespace Entites
{
    public class PeopleEntity
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public byte[] PersonImageData { get; set; }

    }
}
