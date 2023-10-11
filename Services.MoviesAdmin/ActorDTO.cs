namespace Services.MoviesAdmin
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public byte[] PersonImageData { get; set; }
        public string CharacterName { get; set; }
    }
}
