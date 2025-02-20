using IPLManagementSystem.Data;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPLManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamControllerAPI : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeamControllerAPI(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams()
        {
            var teams = await _context.Teams
                .Select(t => new TeamDTO
                {
                    TeamId = t.TeamId,
                    Name = t.TeamName,
                    Coach = t.Coach
                })
                .ToListAsync();

            return Ok(teams);
        }

        // GET: api/team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetTeam(int id)
        {
            var team = await _context.Teams
                .FirstOrDefaultAsync(t => t.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            var teamDTO = new TeamDTO
            {
                TeamId = team.TeamId,
                Name = team.TeamName,
                Coach = team.Coach
            };

            return Ok(teamDTO);
        }

        // POST: api/team
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Team>> CreateTeam(TeamDTO teamDTO)
        {
            if (ModelState.IsValid)
            {
                var team = new Team
                {
                    TeamName = teamDTO.Name,
                    Coach = teamDTO.Coach
                };

                _context.Teams.Add(team);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTeam), new { id = team.TeamId }, team);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/team/5
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTeam(int id, TeamDTO teamDTO)
        {
            if (id != teamDTO.TeamId)
            {
                return BadRequest();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(t => t.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            team.TeamName = teamDTO.Name;
            team.Coach = teamDTO.Coach;

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204 No Content response
        }

        // DELETE: api/team/5
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content response
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.TeamId == id);
        }
    }
}
