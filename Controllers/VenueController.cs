using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Models;
using IPLManagementSystem.DTOs;
using Microsoft.EntityFrameworkCore;
using IPLManagementSystem.Data;

namespace IPLManagementSystem.Controllers
{
    public class VenueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Venue
        public IActionResult Index()
        {
            var venues = _context.Venues.ToList();
            return View(venues);
        }

        // GET: Venue/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = _context.Venues
                .Include(v => v.Matches)
                .FirstOrDefault(v => v.VenueId == id);

            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // GET: Venue/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venue/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VenueDTO venueDTO)
        {
            if (ModelState.IsValid)
            {
                var venue = new Venue
                {
                    Name = venueDTO.VenueName,
                    Location = venueDTO.Location
                };

                _context.Venues.Add(venue);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(venueDTO);
        }

        // GET: Venue/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = _context.Venues.Find(id);

            if (venue == null)
            {
                return NotFound();
            }

            var venueDTO = new VenueDTO
            {
                VenueId = venue.VenueId,
                VenueName = venue.Name,
                Location = venue.Location
            };

            return View(venueDTO);
        }

        // POST: Venue/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, VenueDTO venueDTO)
        {
            if (id != venueDTO.VenueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var venue = _context.Venues.Find(id);

                if (venue == null)
                {
                    return NotFound();
                }

                venue.Name = venueDTO.VenueName;
                venue.Location = venueDTO.Location;

                _context.Update(venue);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(venueDTO);
        }

        // GET: Venue/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = _context.Venues
                .Include(v => v.Matches)
                .FirstOrDefault(v => v.VenueId == id);

            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // POST: Venue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var venue = _context.Venues.Find(id);
            if (venue == null)
            {
                return NotFound();
            }

            _context.Venues.Remove(venue);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}