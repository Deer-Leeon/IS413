using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission06_Buchmiller.Models;

namespace Mission06_Buchmiller.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Security measure to prevent CSRF attacks
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                // Set default value for LentTo if it is null or empty
                if (string.IsNullOrEmpty(movie.LentTo))
                {
                    movie.LentTo = "N/A";
                }

                _context.Movies.Add(movie);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(movie);
        }
    }
}