using Entites.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MovieInfo
{
    public class WatchStatusSelectDTO
    {
        public WatchStatusEnum value {  get; set; }
        public string label { get; set; }

    }
}
