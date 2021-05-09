using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
     [Table("Genre")]
    public class Genre
    {
      
        [Key]
        public int GenreId { get; set; }
        //[Column(TypeName="nvarchar(20)")]
        //public Genres Name  { get; set; }
        [Display(Name="Genre name")]
        public string Name { get; set; }


        public ICollection<Movie> Movies { get; set; }
       
        public List<MovieGenre> MovieGenres { get; set; }



    }
}
