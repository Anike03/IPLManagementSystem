using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Interfaces;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Data;
using Microsoft.AspNetCore.Mvc.Rendering; 

namespace IPLManagementSystem.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly ApplicationDbContext _context; //fetch venues and teams

        public MatchController(IMatchService matchService, ApplicationDbContext context)
        {
            _matchService = matchService;
            _context = context; // Initialize the database context
        }

        public IActionResult Index()
        {
            var matches = _matchService.GetAllMatches();
            return View(matches);
        }

        public IActionResult Details(int id)
        {
            var match = _matchService.GetMatchById(id);
            if (match == null)
                return NotFound();

            return View(match);
        }

        public IActionResult Create()
        {
            // Fetch venues and teams from the database and pass them to the view
            ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "Name");
            ViewBag.Teams = new SelectList(_context.Teams, "TeamId", "TeamName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(MatchDTO matchDTO)
        {
            if (ModelState.IsValid)
            {
                // Ensure exactly two teams are selected
                if (matchDTO.TeamIds == null || matchDTO.TeamIds.Count != 2)
                {
                    ModelState.AddModelError("TeamIds", "Please select exactly two teams.");
                    ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "Name");
                    ViewBag.Teams = new SelectList(_context.Teams, "TeamId", "TeamName");
                    return View(matchDTO);
                }

                _matchService.CreateMatch(matchDTO);
                return RedirectToAction(nameof(Index));
            }

            // If the model state is invalid, repopulate the dropdowns
            ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "Name");
            ViewBag.Teams = new SelectList(_context.Teams, "TeamId", "TeamName");
            return View(matchDTO);
        }

        public IActionResult Edit(int id)
        {
            var match = _matchService.GetMatchById(id);
            if (match == null)
                return NotFound();

            // Map Match to MatchDTO
            var matchDTO = new MatchDTO
            {
                MatchId = match.MatchId,
                MatchDate = match.MatchDate,
                VenueId = match.VenueId,
                TeamIds = match.Teams.Select(t => t.TeamId).ToList()
            };

            // Fetch venues and teams from the database and pass them to the view
            ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "Name");
            ViewBag.Teams = new SelectList(_context.Teams, "TeamId", "TeamName");
            return View(matchDTO);
        }

        [HttpPost]
        public IActionResult Edit(int id, MatchDTO matchDTO)
        {
            if (ModelState.IsValid)
            {
                // Ensure exactly two teams are selected
                if (matchDTO.TeamIds == null || matchDTO.TeamIds.Count != 2)
                {
                    ModelState.AddModelError("TeamIds", "Please select exactly two teams.");
                    ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "Name");
                    ViewBag.Teams = new SelectList(_context.Teams, "TeamId", "TeamName");
                    return View(matchDTO);
                }

                _matchService.UpdateMatch(id, matchDTO);
                return RedirectToAction(nameof(Index));
            }

            // If the model state is invalid, repopulate the dropdowns
            ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "Name");
            ViewBag.Teams = new SelectList(_context.Teams, "TeamId", "TeamName");
            return View(matchDTO);
        }

        public IActionResult Delete(int id)
        {
            var match = _matchService.GetMatchById(id);
            if (match == null)
                return NotFound();

            return View(match);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _matchService.DeleteMatch(id);
            return RedirectToAction(nameof(Index));
        }
    }
}