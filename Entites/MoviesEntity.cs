﻿namespace Entites
{
    public class MoviesEntity
    {
        public Guid Id { get; set; }
        public string MovieName { get; set; }
        public string Synopsis { get; set; }
        public string Genres { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Duration { get; set; }
        public decimal? Rating { get; set; }
        public byte[] MovieImageData { get; set; }
        public DateTime DateTimeAdded { get; set; }

    }
}