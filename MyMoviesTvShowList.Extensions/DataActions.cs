using Microsoft.AspNetCore.Http;

namespace MyMoviesTvShowList.Extensions

{
    public static class DataActions
    {
        public static byte[] ImageToByte(IFormFile image)
        {
            byte[] s = null;
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                s = ms.ToArray();

            }
            return s;
        }

        public static string FormatGenres(string data)
        {
            string genres = null;

            var genresList = data.Split(",").ToList();

            int i = 1;
            foreach (var g in genresList)
            {
                if (i == genresList.Count)
                {
                    genres += g;

                }
                else
                {
                    genres += g + ",";
                }
                i++;
            }
            return genres;
        }

    }
}
