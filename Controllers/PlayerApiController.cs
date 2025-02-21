using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Models; // Add this line
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Interfaces;

namespace IPLManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerApiController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerApiController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Player>> GetPlayers()
        {
            return Ok(_playerService.GetAllPlayers());
        }

        [HttpGet("{id}")]
        public ActionResult<Player> GetPlayer(int id)
        {
            var player = _playerService.GetPlayerById(id);
            if (player == null)
                return NotFound();

            return Ok(player);
        }

        [HttpPost]
        public IActionResult CreatePlayer([FromBody] PlayerDTO playerDTO)
        {
            _playerService.CreatePlayer(playerDTO);
            return CreatedAtAction(nameof(GetPlayer), new { id = playerDTO.PlayerId }, playerDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, [FromBody] PlayerDTO playerDTO)
        {
            if (id != playerDTO.PlayerId)
                return BadRequest();

            _playerService.UpdatePlayer(id, playerDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            _playerService.DeletePlayer(id);
            return NoContent();
        }
    }
}