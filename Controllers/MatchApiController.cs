using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Interfaces;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

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

        // GET: api/MatchApi
        [HttpGet]
        public ActionResult<IEnumerable<Match>> GetMatches()
        {
            var matches = _matchService.GetAllMatches();
            return Ok(matches);
        }

        // GET: api/MatchApi/5
        [HttpGet("{id}")]
        public ActionResult<Match> GetMatch(int id)
        {
            var match = _matchService.GetMatchById(id);
            if (match == null)
            {
                return NotFound();
            }
            return Ok(match);
        }

        // POST: api/MatchApi
        [HttpPost]
        public ActionResult<Match> CreateMatch([FromBody] MatchDTO matchDTO)
        {
            if (matchDTO.TeamIds == null || matchDTO.TeamIds.Count != 2)
            {
                return BadRequest("Please select exactly two teams.");
            }

            _matchService.CreateMatch(matchDTO);
            return CreatedAtAction(nameof(GetMatch), new { id = matchDTO.MatchId }, matchDTO);
        }

        // PUT: api/MatchApi/5
        [HttpPut("{id}")]
        public IActionResult UpdateMatch(int id, [FromBody] MatchDTO matchDTO)
        {
            if (matchDTO.TeamIds == null || matchDTO.TeamIds.Count != 2)
            {
                return BadRequest("Please select exactly two teams.");
            }

            _matchService.UpdateMatch(id, matchDTO);
            return NoContent();
        }

        // DELETE: api/MatchApi/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMatch(int id)
        {
            _matchService.DeleteMatch(id);
            return NoContent();
        }
    }
}