using IPLManagementSystem.Data;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPLManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchControllerAPI : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MatchControllerAPI(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/match
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchDTO>>> GetMatches()
        {
            var matches = await _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.Teams)
                .Select(m => new MatchDTO
                {
                    MatchId = m.MatchId,
                    MatchDate = m.MatchDate,
                    VenueId = m.VenueId,
                    TeamIds = m.Teams.Select(t => t.TeamId).ToList()
                })
                .ToListAsync();

            return Ok(matches);
        }

        // GET: api/match/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDTO>> GetMatch(int id)
        {
            var match = await _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.Teams)
                .FirstOrDefaultAsync(m => m.MatchId == id);

            if (match == null)
            {
                return NotFound(new { Message = $"Match with ID {id} not found." });
            }

            var matchDTO = new MatchDTO
            {
                MatchId = match.MatchId,
                MatchDate = match.MatchDate,
                VenueId = match.VenueId,
                TeamIds = match.Teams.Select(t => t.TeamId).ToList()
            };

            return Ok(matchDTO);
        }

        // POST: api/match
        [HttpPost]
        public async Task<ActionResult<Match>> CreateMatch(MatchDTO matchDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var match = new Match
            {
                MatchDate = matchDTO.MatchDate,
                VenueId = matchDTO.VenueId,
                Teams = await _context.Teams
                    .Where(t => matchDTO.TeamIds.Contains(t.TeamId))
                    .ToListAsync()
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMatch), new { id = match.MatchId }, match);
        }

        // PUT: api/match/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatch(int id, MatchDTO matchDTO)
        {
            if (id != matchDTO.MatchId)
            {
                return BadRequest(new { Message = "Match ID mismatch." });
            }

            var match = await _context.Matches
                .Include(m => m.Teams)
                .FirstOrDefaultAsync(m => m.MatchId == id);

            if (match == null)
            {
                return NotFound(new { Message = $"Match with ID {id} not found." });
            }

            match.MatchDate = matchDTO.MatchDate;
            match.VenueId = matchDTO.VenueId;
            match.Teams = await _context.Teams
                .Where(t => matchDTO.TeamIds.Contains(t.TeamId))
                .ToListAsync();

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
                {
                    return NotFound(new { Message = $"Match with ID {id} not found." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204 No Content response
        }

        // DELETE: api/match/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound(new { Message = $"Match with ID {id} not found." });
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content response
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.MatchId == id);
        }
    }
}