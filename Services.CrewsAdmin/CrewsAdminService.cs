using DatabaseContext;
using Entites;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MyMoviesTvShowList.Extensions;

namespace Services.CrewsAdmin
{
    public class CrewsAdminService : ICrewsAdminService
    {
        private readonly MyMoviesTvShowListContext database;

        public CrewsAdminService(MyMoviesTvShowListContext database)
        {
            this.database = database;
        }

        public async Task<List<PeopleEntity>> GetPeople(int PostPerPage, int Page, string? Search)
        {
            Expression<Func<PeopleEntity, bool>> predicate = x => true;

            if (!String.IsNullOrEmpty(Search))
            {
                predicate = x => x.FirstName.Contains(Search) || x.LastName.Contains(Search);
            }

            var people = await database.People
                .Where(predicate)
                .OrderBy(o => o.FirstName)
                .Select(p =>
                new PeopleEntity
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    BirthDate = p.BirthDate,
                    BirthPlace = p.BirthPlace,
                    PersonImageData = p.PersonImageData
                }
                )
                .Skip((Page - 1) * PostPerPage)
                .Take(PostPerPage)
                .ToListAsync();
            return people;
        }

        public async Task<int> GetPeopleCount()
        {
            return await database.People.CountAsync();
        }

        public async Task SavePerson(PersonDTO person)
        {
            if (person.Id > 0)
            {
                var personDb = await database.People.Where(w => w.Id == person.Id).FirstOrDefaultAsync();
                personDb.FirstName = person.FirstName.Trim();
                personDb.LastName = person.LastName.Trim();
                personDb.BirthDate = person.BirthDate.ToUniversalTime();
                personDb.BirthPlace = person.BirthPlace.Trim();

                if (person.PersonImage.Length > 0)
                {
                    personDb.PersonImageData = DataActions.ImageToByte(person.PersonImage);
                }
                database.Update(personDb);
            }
            else
            {
                byte[] s = DataActions.ImageToByte(person.PersonImage);

                await database.People.AddAsync(new PeopleEntity
                {
                    FirstName = person.FirstName.Trim(),
                    LastName = person.LastName.Trim(),
                    BirthDate = person.BirthDate.ToUniversalTime(),
                    BirthPlace = person.BirthPlace.Trim(),
                    PersonImageData = s
                });
            }

            await database.SaveChangesAsync();
        }


    }
}
