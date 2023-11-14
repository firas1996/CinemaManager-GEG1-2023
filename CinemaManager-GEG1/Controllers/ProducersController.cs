using CinemaManager_GEG1.Models.Cinema;
using CinemaManager_GEG1.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaManager_GEG1.Controllers
{
    public class ProducersController : Controller
    {
        CinemaDbgeg1Context _context;
        public ProducersController(CinemaDbgeg1Context context)
        {
            _context = context;
        }
        // GET: ProducersController
        public ActionResult Index()
        {
            return View(_context.Producers.ToList());
        }
        public ActionResult ProdsAndTheirMovies()
        {
            _context.Movies.ToList();
            return View(_context.Producers.ToList());
        }
        public IActionResult ProdsAndTheirMovies_UsingModel()
        {
            var movies = _context.Movies.ToList();
            var prods = _context.Producers.ToList();
            var query = from m in movies
                        join p in prods on m.ProducerId equals p.Id
                        select new ProdMovie
                        {
                            mTitle = m.Title,
                            mGenre = m.Genre,
                            pName = p.Name,
                            pNat = p.Nationality
                        };
            return View(query.ToList());
        }

        public IActionResult MyMovies(int id)
        {
            List<Movie> PM = _context.Movies.Where(m => m.ProducerId == id).ToList();
            return View(PM);
        }

        // GET: ProducersController/Details/5
        public ActionResult Details(int id)
        {
            Producer prod = _context.Producers.Find(id);
            return View(prod);
        }

        // GET: ProducersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProducersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producer p)
        {
            try
            {
                _context.Producers.Add(p);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProducersController/Edit/5
        public ActionResult Edit(int id)
        {
            Producer prod = _context.Producers.Find(id);
            return View(prod);
        }

        // POST: ProducersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Producer p)
        {
            try
            {
                _context.Producers.Update(p);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProducersController/Delete/5
        public ActionResult Delete(int id)
        {
            Producer prod = _context.Producers.Find(id);
            return View(prod);
        }

        // POST: ProducersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Producer prod)
        {
            try
            {
                _context.Producers.Remove(prod);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
