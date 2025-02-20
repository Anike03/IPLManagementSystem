using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Models;
using IPLManagementSystem.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using IPLManagementSystem.Data;

namespace IPLManagementSystem.Controllers
{
    public class MatchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Match
        public IActionResult Index()
        {
            var matches = _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.Teams)
                .ToList();
            return View(matches);
        }

        // GET: Match/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.Teams)
                .FirstOrDefault(m => m.MatchId == id);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Match/Create
        public IActionResult Create()
        {
            ViewBag.Venues = _context.Venues.ToList();
            ViewBag.Teams = _context.Teams.ToList();
            return View();
        }

        // POST: Match/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MatchDTO matchDTO)
        {
            if (ModelState.IsValid)
            {
                var match = new Match
                {
                    MatchDate = matchDTO.MatchDate,
                    VenueId = matchDTO.VenueId,
                    Teams = _context.Teams.Where(t => matchDTO.TeamIds.Contains(t.TeamId)).ToList()
                };

                _context.Matches.Add(match);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Venues = _context.Venues.ToList();
            ViewBag.Teams = _context.Teams.ToList();
            return View(matchDTO);
        }

        // GET: Match/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.Teams)
                .FirstOrDefault(m => m.MatchId == id);

            if (match == null)
            {
                return NotFound();
            }

            var matchDTO = new MatchDTO
            {
                MatchId = match.MatchId,
                MatchDate = match.MatchDate,
                VenueId = match.VenueId,
                TeamIds = match.Teams.Select(t => t.TeamId).ToList()
            };

            ViewBag.Venues = _context.Venues.ToList();
            ViewBag.Teams = _context.Teams.ToList();
            return View(matchDTO);
        }

        // POST: Match/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MatchDTO matchDTO)
        {
            if (id != matchDTO.MatchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var match = _context.Matches
                    .Include(m => m.Teams)
                    .FirstOrDefault(m => m.MatchId == id);

                if (match == null)
                {
                    return NotFound();
                }

                match.MatchDate = matchDTO.MatchDate;
                match.VenueId = matchDTO.VenueId;
                match.Teams = _context.Teams.Where(t => matchDTO.TeamIds.Contains(t.TeamId)).ToList();

                _context.Update(match);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Venues = _context.Venues.ToList();
            ViewBag.Teams = _context.Teams.ToList();
            return View(matchDTO);
        }

        // GET: Match/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.Teams)
                .FirstOrDefault(m => m.MatchId == id);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var match = _context.Matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}