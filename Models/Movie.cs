using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Movies.Models
{

    public partial class Movie
    {
        public Movie()
        {
            this.Genres = new List<Genre>();
        }
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Storyline { get; set; }    
        public int? Year { get; set; }
        [DataType(DataType.Date)]
        [Validators(ErrorMessage = "Date must be after or equal to current date")]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }
        public int? Runtime { get; set; }
        [Display(Name = "Movie Type")]
        [Column(TypeName = "nvarchar(20)")]
        public MovieType MovieType { get; set; }
   
        public ICollection<Genre> Genres { get; set; }
   
       public List<MovieGenre> MovieGenres { get; set; }




    }


}

