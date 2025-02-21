using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Models;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Services;
using IPLManagementSystem.Interfaces;

namespace IPLManagementSystem.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        // GET: Match
        public IActionResult Index()
        {
            var matches = _matchService.GetAllMatches();
            return View(matches);
        }

        // GET: Match/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = _matchService.GetMatchById(id.Value);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Match/Create
        public IActionResult Create()
        {
            ViewBag.Venues = _matchService.GetVenues();
            ViewBag.Teams = _matchService.GetTeams();
            return View();
        }

        // POST: Match/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MatchDTO matchDTO)
        {
            if (ModelState.IsValid)
            {
                _matchService.CreateMatch(matchDTO);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Venues = _matchService.GetVenues();
            ViewBag.Teams = _matchService.GetTeams();
            return View(matchDTO);
        }

        // GET: Match/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = _matchService.GetMatchById(id.Value);
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

            ViewBag.Venues = _matchService.GetVenues();
            ViewBag.Teams = _matchService.GetTeams();
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
                _matchService.UpdateMatch(id, matchDTO);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Venues = _matchService.GetVenues();
            ViewBag.Teams = _matchService.GetTeams();
            return View(matchDTO);
        }

        // GET: Match/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = _matchService.GetMatchById(id.Value);
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
            _matchService.DeleteMatch(id);
            return RedirectToAction(nameof(Index));
        }
    }
}