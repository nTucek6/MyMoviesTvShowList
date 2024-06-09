using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ExternalApiCalls
{
    public class MediaApiDTO
    {
        public string Title { get; set; }
        public string Plot { get; set; }
        public string Runtime { get; set; }
        public string Released { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }

    }
}
