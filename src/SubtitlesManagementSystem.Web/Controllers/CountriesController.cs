using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubtitlesManagementSystem.Web.Data;
using SubtitlesManagementSystem.Web.Models;

namespace SubtitlesManagementSystem.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CountriesController(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        // GET: Countries
        public IActionResult Index()
        {
            if (_applicationDbContext.Countries != null)
            {
                var allCountries = _applicationDbContext.Countries.ToList();
                return View(allCountries);
            }

            return Problem("Entity set 'ApplicationDbContext.Countries'  is null.");
        }

        // GET: Countries/Details/0f8fad5b
        public IActionResult Details(string id)
        {
            if (id == null || _applicationDbContext.Countries == null)
            {
                return NotFound();
            }

            var countryDetails = _applicationDbContext.Countries.FirstOrDefault(c => c.Id == id);

            if (countryDetails == null)
            {
                return NotFound();
            }

            return View(countryDetails);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Country country)
        {
            if (ModelState.IsValid)
            {
                _applicationDbContext.Add(country);
                _applicationDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        // GET: Countries/Edit/0f8fad5b
        public IActionResult Edit(string id)
        {
            if (id == null || _applicationDbContext.Countries == null)
            {
                return NotFound();
            }

            var countryToEditDetails = _applicationDbContext.Countries.Find(id);

            if (countryToEditDetails == null)
            {
                return NotFound();
            }

            return View(countryToEditDetails);
        }

        // POST: Countries/Edit/0f8fad5b
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,Name")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _applicationDbContext.Update(country);
                    _applicationDbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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

            return View(country);
        }

        // GET: Countries/Delete/0f8fad5b
        public IActionResult Delete(string id)
        {
            if (id == null || _applicationDbContext.Countries == null)
            {
                return NotFound();
            }

            var countryToDeleteDetails = _applicationDbContext.Countries.FirstOrDefault(c => c.Id == id);

            if (countryToDeleteDetails == null)
            {
                return NotFound();
            }

            return View(countryToDeleteDetails);
        }

        // POST: Countries/Delete/0f8fad5b
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            if (_applicationDbContext.Countries == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Countries'  is null.");
            }

            var countryToConfirmDeletion = _applicationDbContext.Countries.Find(id);

            if (countryToConfirmDeletion != null)
            {
                _applicationDbContext.Countries.Remove(countryToConfirmDeletion);
            }
            
            _applicationDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(string id)
        {
            return (_applicationDbContext.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
