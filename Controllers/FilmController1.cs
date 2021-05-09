using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    public class FilmController : Controller
    {
        // GET: FilmController1
        private readonly MovieContext _context;

        public FilmController(MovieContext context)
        {
            _context = context;
        }

        //GET: Products
        public async Task<IActionResult> Index()
        {
     
            var course = _context.Movies
           .Include(c => c.Genres)
           .AsNoTracking();
           
            return View(await course.ToListAsync());


        }


     
     

        // GET: Products/Create
        public IActionResult Create()
        {


            //used to get a table as check box from another table which have a foreign key
            ViewBag.GenreId = new MultiSelectList(_context.Genres.ToList(), "GenreId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to. For 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       public async Task<IActionResult> Create([Bind("Title,Description,Storyline,Year,ReleaseDate,Runtime,MovieType,GenreId")] Movie movie)
        {
           

            if (ModelState.IsValid)
            {
               
                var body = Request.Form["GenreId"];
                foreach (var g in body.ToList())
                {
                    var gen = _context.Genres.First(x => x.GenreId == int.Parse(g));
                    movie.Genres.Add(gen);
                }

                _context.Movies.Add(movie);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.GenreId = new MultiSelectList(_context.Genres.ToList(), "GenreId", "Name"); //For Genre as a Checkbox list
            return View(movie);
           
            
        }

     

       

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           // ViewBag.GenreId = new SelectList(_context.Genre, "GenreId", "Name");//the soure of dropdownlist
            var product = await _context.Movies.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to. For 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,Description,Storyline,Year,ReleaseDate,Runtime,MovieType")] Movie updatedProductDetails)
        {
            if (id != updatedProductDetails.MovieId)
            {
                return NotFound();
            }

            var Movies = await _context.Movies.FindAsync(id);

            if (Movies == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Movies.MovieId = updatedProductDetails.MovieId;
                    Movies.Title = updatedProductDetails.Title;
                    Movies.Description = updatedProductDetails.Description;
                    Movies.Storyline = updatedProductDetails.Storyline;
                    Movies.Year = updatedProductDetails.Year;
                    Movies.ReleaseDate = updatedProductDetails.ReleaseDate;
                    Movies.Runtime = updatedProductDetails.Runtime;
                    Movies.MovieType = updatedProductDetails.MovieType;

                   // Movies.GenreId = updatedProductDetails.GenreId;





                    _context.Update(Movies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(updatedProductDetails.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               // ViewBag.GenreId = new SelectList(_context.Genre, "GenreId", "Name");//the soure of dropdownlist
                ViewBag.GenreId = new SelectList(_context.Genres, "GenreId", "Name");//the soure of dropdownlist

                return RedirectToAction(nameof(Index));
            }

            return View(updatedProductDetails);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == id);
        

            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Movies.FindAsync(id);
           
            _context.Movies.Remove(product);
           
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(long id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }

        //GET SEARCH
        public async Task<IActionResult>Search()
        {

            return View();
        }


        }
}
