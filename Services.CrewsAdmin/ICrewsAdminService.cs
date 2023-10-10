using Entites;

namespace Services.CrewsAdmin
{
    public interface ICrewsAdminService
    {
        Task SavePerson(PersonDTO person);
        Task<List<PeopleEntity>> GetPeople(int PostPerPage, int Page, string? Search);
        Task<int> GetPeopleCount();
    }
}
