using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieContext _context;

        public HomeController(MovieContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //var Movies = await _context.Genre.ToListAsync();
            var Movies = await _context.Genres.ToListAsync();
            return View(Movies);
        }

        public IActionResult Create()
        {
            //ViewBag.GenreId = new SelectList(, "GenreId", "Name");//the soure of dropdownlist
           

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreId,Name")] Genre Genres)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Genres);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(Genres);



        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           // var product = await _context.Genre.FindAsync(id);
            var product = await _context.Genres.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("GenreId,Name")] Genre updatedProductDetails)
        {
            if (id != updatedProductDetails.GenreId)
            {
                return NotFound();
            }

           // var Movies = await _context.Genre.FindAsync(id);
              var Movies = await _context.Genres.FindAsync(id);

            if (Movies == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Movies.GenreId = updatedProductDetails.GenreId;
                    Movies.Name = updatedProductDetails.Name;
                  





                    _context.Update(Movies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(updatedProductDetails.GenreId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(updatedProductDetails);
        }




        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var Genres = await _context.Genre.FirstOrDefaultAsync(m => m.GenreId == id);
            var Genres = await _context.Genres.FirstOrDefaultAsync(m => m.GenreId == id);
            if (Genres == null)
            {
                return NotFound();
            }

            return View(Genres);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           // var Genres = await _context.Genre.FindAsync(id);
            var Genres = await _context.Genres.FindAsync(id);
            //_context.Genre.Remove(Genres);
             _context.Genres.Remove(Genres);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(long id)
        {
            //return _context.Genre.Any(e => e.GenreId == id);
            return _context.Genres.Any(e => e.GenreId == id);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
