using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Interfaces;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Models;
using System.Collections.Generic;

namespace IPLManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchApiController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchApiController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        // GET: api/match
        [HttpGet]
        public ActionResult<IEnumerable<Match>> GetMatches()
        {
            return Ok(_matchService.GetAllMatches());
        }

        // GET: api/match/{id}
        [HttpGet("{id}")]
        public ActionResult<Match> GetMatch(int id)
        {
            var match = _matchService.GetMatchById(id);
            if (match == null)
            {
                return NotFound(new { message = "Match not found" });
            }
            return Ok(match);
        }

        // POST: api/match
        [HttpPost]
        public ActionResult CreateMatch([FromBody] MatchDTO matchDTO)
        {
            if (matchDTO.TeamIds == null || matchDTO.TeamIds.Count != 2)
            {
                return BadRequest(new { message = "Exactly two teams must be selected." });
            }

            _matchService.CreateMatch(matchDTO);
            return CreatedAtAction(nameof(GetMatch), new { id = matchDTO.MatchId }, matchDTO);
        }

        // PUT: api/match/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateMatch(int id, [FromBody] MatchDTO matchDTO)
        {
            if (matchDTO.TeamIds == null || matchDTO.TeamIds.Count != 2)
            {
                return BadRequest(new { message = "Exactly two teams must be selected." });
            }

            try
            {
                _matchService.UpdateMatch(id, matchDTO);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Match not found" });
            }
        }

        // DELETE: api/match/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMatch(int id)
        {
            try
            {
                _matchService.DeleteMatch(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Match not found" });
            }
        }
    }
}
