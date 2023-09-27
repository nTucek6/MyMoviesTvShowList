using Microsoft.EntityFrameworkCore;
using Entites;

namespace DatabaseContext
{
    public class MyMoviesTvShowListContext : DbContext
    {
        public MyMoviesTvShowListContext(DbContextOptions<MyMoviesTvShowListContext> options) : base(options)
        {
        }

        public DbSet<MoviesEntity> Movies { get; set; }
        public DbSet<UsersEntity> Users { get; set; }
        public DbSet<MoviesCrewEntity> MoviesCrew { get; set; }

    }
}