using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Interfaces;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace IPLManagementSystem.Controllers.Api
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

        // GET: api/PlayerApi
        [HttpGet]
        public ActionResult<IEnumerable<Player>> GetAllPlayers()
        {
            var players = _playerService.GetAllPlayers();
            return Ok(players);
        }

        // GET: api/PlayerApi/5
        [HttpGet("{id}")]
        public ActionResult<Player> GetPlayerById(int id)
        {
            var player = _playerService.GetPlayerById(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        // POST: api/PlayerApi
        [HttpPost]
        public ActionResult<Player> CreatePlayer([FromBody] PlayerDTO playerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _playerService.CreatePlayer(playerDTO);
            return CreatedAtAction(nameof(GetPlayerById), new { id = playerDTO.PlayerId }, playerDTO);
        }

        // PUT: api/PlayerApi/5
        [HttpPut("{id}")]
        public ActionResult UpdatePlayer(int id, [FromBody] PlayerDTO playerDTO)
        {
            if (id != playerDTO.PlayerId)
            {
                return BadRequest("Player ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _playerService.UpdatePlayer(id, playerDTO);
            return NoContent();
        }

        // DELETE: api/PlayerApi/5
        [HttpDelete("{id}")]
        public ActionResult DeletePlayer(int id)
        {
            var player = _playerService.GetPlayerById(id);
            if (player == null)
            {
                return NotFound();
            }

            _playerService.DeletePlayer(id);
            return NoContent();
        }
    }
}