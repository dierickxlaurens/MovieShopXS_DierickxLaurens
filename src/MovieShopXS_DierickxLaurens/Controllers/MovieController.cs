using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieShopXS_DierickxLaurens.entities;
using MovieShopXS_DierickxLaurens.ModelView;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopXS_DierickxLaurens.Controllers
{
    [Route("")]
    [Route("Movies")]
    public class MovieController : Controller
    {
        private MovieContext db;
        public MovieController(MovieContext mc)
        {
            db = mc;
        }
        // GET: /<controller>/
        [Route("")]
        public IActionResult Movies()
        {
            return View(
                db.Movie.Select(e => new MovieView {
                    Titel = e.Title,
                    Jaar = e.Year,
                    Beschrijving = e.Description,
                    Acteurs = e.MovieActor.Select(a => new ActeurView { Naam = a.Actor.FirstName + " " + a.Actor.LastName }).ToList(),
                    Image = e.Title + ".jpg",
                    MovieId = e.MovieId,
                    Regisseur = e.Director.FirstName + " " + e.Director.LastName,
                    Rating = e.Stars
                })
            .ToList()
            );
        }
        [Route("Year/{jaar?}")]
        public IActionResult Year(int jaar)
        {
            ICollection<MovieView> list;

            list= db.Movie.Where(e => e.Year == jaar)
                .Select(e => new MovieView
                {
                    Titel = e.Title,
                    Jaar = e.Year,
                    Beschrijving = e.Description,
                    Acteurs = e.MovieActor.Select(a => new ActeurView { Naam = a.Actor.FirstName + " " + a.Actor.LastName }).ToList(),
                    Image = e.Title + ".jpg",
                    MovieId = e.MovieId,
                    Regisseur = e.Director.FirstName + " " + e.Director.LastName,
                    Rating = e.Stars
                }).ToList();

            if (list.ToArray().Length > 0)
            {
                return View("Movies", list);
            }
            return View("NoMovies");
        }
        [Route("Update/{id?}/Rating/{Direction?}")]
        public IActionResult Update(int id,string direction)
        {
            Movie movie = (from m in db.Movie
                       where m.MovieId == id select m).First();
            if(direction == "Up")
            {
                movie.Stars += (Byte) 1;
            }
            else
            {
                movie.Stars -= (Byte)1;
            }
            db.SaveChanges();
            return RedirectToAction("Movies");
        }
    }
}
