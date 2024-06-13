using Microsoft.Extensions.Configuration;
using System.Text.Json;
using MyMoviesTvShowList.Extensions;
using Services.CrewsAdmin;
using System.Globalization;
using Services.MoviesAdmin;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Entites;
using Entities.Enum;
using Services.TVShowsAdmin;


namespace Services.ExternalApiCalls
{
    public class ExternalApiCallsService : IExternalApiCallsService
    {

        private readonly string OMDbAPIUrl;
        private readonly string OMDbAPISecret;

        private readonly string ApiNinjas;
        private readonly string ApiNinjasSecret;

        private readonly MyMoviesTvShowListContext database;

        public ExternalApiCallsService(MyMoviesTvShowListContext myMoviesTvShowListContext, IConfiguration configuration)
        {
            database = myMoviesTvShowListContext;
            OMDbAPIUrl = configuration.GetSection("OMDbAPI:BaseUrl").Value;
            OMDbAPISecret = configuration.GetSection("OMDbAPI:ApiKey").Value;
            ApiNinjas = configuration.GetSection("APINinjas:BaseUrl").Value;
            ApiNinjasSecret = configuration.GetSection("APINinjas:ApiKey").Value;
        }

        public async Task<PersonDTO> GetCelebrity(string Fullname)
        {
            PersonDTO celebrity = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ApiNinjas);

            string query = $"?name={Fullname}";

            client.DefaultRequestHeaders.Add("X-Api-Key", ApiNinjasSecret);

            var response = await client.GetAsync(query);

            string responseBody = await response.Content.ReadAsStringAsync();

            List<CelebrityDTO> data = JsonSerializer.Deserialize<List<CelebrityDTO>>(responseBody);

            if (data.Count == 1)
            {
                string[] nameParts = data[0].name.Split(' ');

                string firstname = nameParts[0].ElementAt(0).ToString().ToUpper() + nameParts[0].Substring(1);
                string lastname = nameParts[1].ElementAt(0).ToString().ToUpper() + nameParts[1].Substring(1);

                string format = "yyyy-MM-dd";

                DateTime parsedDate = DateTime.ParseExact(data[0].birthday, format, CultureInfo.InvariantCulture);

                celebrity = new PersonDTO
                {
                    Id = 0,
                    FirstName = firstname,
                    LastName = lastname,
                    BirthDate = parsedDate,
                    BirthPlace = null,
                };
            }
            return celebrity;
        }

          public async Task<MoviesDTO> GetMovieFromApi(string Title)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(OMDbAPIUrl);

            string query = $"?apikey={OMDbAPISecret}&t={Title}&type=movie&r=json";

            var response = await client.GetAsync(query);

            string responseBody = await response.Content.ReadAsStringAsync();

            MediaApiDTO data = JsonSerializer.Deserialize<MediaApiDTO>(responseBody);

            if (Convert.ToBoolean(data.Response))
            {
                List<ActorDTO> actors = await GetActorsFromList(data.Actors);
                List<PeopleEntity> directors = await GetPeopleFromList(data.Director);
                List<PeopleEntity> writers = await GetPeopleFromList(data.Writer);

                List<GenresSelectDTO> genres = new List<GenresSelectDTO>();

                if (data.Genre != null)
                {
                    foreach (string g in data.Genre.Split(",").ToList())
                    {
                        var genre = SearchGenre(g.Trim());
                        if (genre != null)
                        {
                            genres.Add(genre);
                        }
                    }
                }

                int duration = Convert.ToInt32(data.Runtime.Split(" ").ToList()[0]);
                DateTime date = DateTime.ParseExact(data.Released, "dd MMM yyyy", CultureInfo.InvariantCulture);

                //Get img as byte
                client = new HttpClient();
                response = await client.GetAsync(data.Poster);
                byte[] imageBytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                MoviesDTO movie = null;

                movie = new MoviesDTO
                {
                    Id = 0,
                    MovieName = data.Title,
                    Actors = actors,
                    Director = directors,
                    Writers = writers,
                    Duration = duration,
                    ReleaseDate = date,
                    Synopsis = data.Plot,
                    Genres = genres,
                    MovieImageData = imageBytes,
                };
                return movie;
            }
            else
            {
                throw new Exception(data.Error);
            }
        }


        public async Task<TVShowDTO> GetTVShowFromApi(string Title)
        {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(OMDbAPIUrl);

                string query = $"?apikey={OMDbAPISecret}&t={Title}&type=series&r=json";

                var response = await client.GetAsync(query);

                string responseBody = await response.Content.ReadAsStringAsync();

                MediaApiDTO data = JsonSerializer.Deserialize<MediaApiDTO>(responseBody);

                if (Convert.ToBoolean(data.Response))
                {
                    List<ActorDTO> actors = await GetActorsFromList(data.Actors);
                    List<PeopleEntity> writers = await GetPeopleFromList(data.Writer);

                    List<GenresSelectDTO> genres = new List<GenresSelectDTO>();

                    
                if(data.Genre != null)
                {
                    foreach (string g in data.Genre.Split(",").ToList())
                    {
                        var genre = SearchGenre(g.Trim());
                        if (genre != null)
                        {
                            genres.Add(genre);
                        }
                    }
                }

                    //int runtime = Convert.ToInt32(data.Runtime.Split(" ").ToList()[0]);
                    DateTime date = DateTime.ParseExact(data.Released, "dd MMM yyyy", CultureInfo.InvariantCulture);

                    //Get img as byte
                    client = new HttpClient();
                    response = await client.GetAsync(data.Poster);
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                    TVShowDTO tVShowDTO = new TVShowDTO
                    {
                        Id = 0,
                        Title = data.Title,
                        Description = data.Plot,
                        TotalSeason = Convert.ToInt32(data.totalSeasons),
                        TotalEpisode = 0,
                        TVShowImageData = imageBytes,
                        Creators = writers,
                        Actors = actors,
                        Genres = genres,
                        Runtime = data.Year
                    };
                    return tVShowDTO;

                }
                else
                {
                throw new Exception(data.Error);
                }
        }

        public GenresSelectDTO SearchGenre(string Genre)
        {
            var genres = Enum.GetValues(typeof(GenresEnum)).Cast<GenresEnum>().ToList().Where(q => q.GetDescription().Contains(Genre)).Select(x => new GenresSelectDTO { value = x, label = x.GetDescription() }).FirstOrDefault();
            return genres;
        }

        private async Task<List<PeopleEntity>> GetPeopleFromList(string data)
        {
            List<PeopleEntity> people = new List<PeopleEntity>();

            if(data != null)
            {
                foreach (string writer in data.Split(',').ToList())
                {
                    var a = await database.People.Where(q => (q.FirstName + " " + q.LastName).ToLower().Contains(writer.ToLower().Trim())).FirstOrDefaultAsync();
                    if (a != null)
                    {
                        people.Add(new PeopleEntity
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                        });
                    }
                }
            }
            return people;
        }

        private async Task<List<ActorDTO>> GetActorsFromList(string data)
        {
            List<ActorDTO> actors = new List<ActorDTO>();
            if(data != null)
            {
                foreach (string actor in data.Split(",").ToList())
                {
                    var a = await database.People.Where(q => (q.FirstName + " " + q.LastName).ToLower().Contains(actor.ToLower().Trim())).FirstOrDefaultAsync();
                    if (a != null)
                    {
                        actors.Add(new ActorDTO
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                        });
                    }
                }
            }
            return actors;
        }

    }
}
