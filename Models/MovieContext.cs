using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


#nullable disable

namespace Movies.Models
{
    public partial class MovieContext : DbContext
    {
      

        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        //public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");



            //modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.GenreId });

            //modelBuilder.Entity<MovieGenre>()
            //            .HasOne(mg => mg.Movie)
            //            .WithMany(m => m.MovieGenre)
            //            .HasForeignKey(g => g.MovieId);

            //modelBuilder.Entity<MovieGenre>()
            //            .HasOne(mg => mg.Genre)
            //            .WithMany(m => m.MovieGenre)
            //            .HasForeignKey(g => g.GenreId);


            //modelBuilder
            //          .Entity<Movie>()
            //          .HasMany(p => p.Genres)
            //          .WithMany(p => p.Movies)
            //          .UsingEntity<Dictionary<string, object>>(
            //            "MovieGenre",
            //            x => x.HasOne<Genre>().WithMany(),
            //            x => x.HasOne<Movie>().WithMany());


            //modelBuilder
            //           .Entity<Movie>()
            //           .HasMany(p => p.Genres)
            //           .WithMany(p => p.Movies)
            //           .UsingEntity(j => j.ToTable("MovieGenre"));


            //modelBuilder
            //       .Entity<Movie>()
            //       .HasMany(m => m.Genres)
            //       .WithMany(p => p.Movies)
            //       .UsingEntity<MovieGenre>(

            // j => j.HasOne(m => m.Genres).WithMany(g => g.MovieGenres),
            // j => j.HasOne(p => p.Movies).WithMany(g => g.MovieGenres));







            modelBuilder
                        .Entity<Movie>()
                        .HasMany(p => p.Genres)
                        .WithMany(p => p.Movies)
                        .UsingEntity(j => j.ToTable("MovieGenre"));


            modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Movies)
            .UsingEntity<MovieGenre>(
                j => j
                    .HasOne(mg => mg.Genre)
                    .WithMany(g => g.MovieGenres)
                    .HasForeignKey(mg => mg.GenreId),
                j => j
                    .HasOne(mg => mg.Movie)
                    .WithMany(g => g.MovieGenres)
                    .HasForeignKey(mg => mg.MovieId),
                j =>
                {
                    j.HasKey(t => new { t.MovieGenreId });
                });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
