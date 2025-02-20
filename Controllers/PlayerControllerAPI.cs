using IPLManagementSystem.Data;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPLManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerControllerAPI : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlayerControllerAPI(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayers()
        {
            var players = await _context.Players
                .Include(p => p.Team)  // Include related Team data
                .Select(p => new PlayerDTO
                {
                    PlayerId = p.PlayerId,
                    Name = p.Name,
                    Age = p.Age,
                    Role = p.Role,
                    TeamId = p.TeamId
                })
                .ToListAsync();

            return Ok(players);
        }

        // GET: api/player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayer(int id)
        {
            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.PlayerId == id);

            if (player == null)
            {
                return NotFound();
            }

            var playerDTO = new PlayerDTO
            {
                PlayerId = player.PlayerId,
                Name = player.Name,
                Age = player.Age,
                Role = player.Role,
                TeamId = player.TeamId
            };

            return Ok(playerDTO);
        }

        // POST: api/player
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Player>> CreatePlayer(PlayerDTO playerDTO)
        {
            if (ModelState.IsValid)
            {
                var player = new Player
                {
                    Name = playerDTO.Name,
                    Age = playerDTO.Age,
                    Role = playerDTO.Role,
                    TeamId = playerDTO.TeamId
                };

                _context.Players.Add(player);
                await _context.SaveChangesAsync();

                // Return 201 Created with the URI of the new player
                return CreatedAtAction(nameof(GetPlayer), new { id = player.PlayerId }, player);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/player/5
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePlayer(int id, PlayerDTO playerDTO)
        {
            if (id != playerDTO.PlayerId)
            {
                return BadRequest();
            }

            var player = await _context.Players
                .FirstOrDefaultAsync(p => p.PlayerId == id);

            if (player == null)
            {
                return NotFound();
            }

            player.Name = playerDTO.Name;
            player.Age = playerDTO.Age;
            player.Role = playerDTO.Role;
            player.TeamId = playerDTO.TeamId;

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // DELETE: api/player/5
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content response
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }
    }
}
