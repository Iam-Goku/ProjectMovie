using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class MovieGenre
    {
        public int MovieGenreId { get; set; }
        
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
