using Microsoft.EntityFrameworkCore;
using Entites;
using Entites.Movie;
using Entites.Show;

namespace DatabaseContext
{
    public class MyMoviesTvShowListContext : DbContext
    {
        public MyMoviesTvShowListContext(DbContextOptions<MyMoviesTvShowListContext> options) : base(options)
        {
        }
        public DbSet<UsersEntity> Users { get; set; }
        public DbSet<PeopleEntity> People { get; set; }

        public DbSet<MoviesEntity> Movies { get; set; }
        public DbSet<MoviesCrewEntity> MoviesCrew { get; set; }
        public DbSet<MoviesCharactersEntity> MoviesCharacters { get; set; }

        public DbSet<TvShowEntity> TvShow { get; set; }
        public DbSet<TvShowCrewEntity> TvShowCrew { get; set; }
        public DbSet<TvShowCharactersEntity> TvShowCharacters { get; set;}


    }
}