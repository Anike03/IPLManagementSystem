using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Models; // Add this line
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Interfaces;

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

        [HttpGet]
        public ActionResult<IEnumerable<Match>> GetMatches()
        {
            return Ok(_matchService.GetAllMatches());
        }

        [HttpGet("{id}")]
        public ActionResult<Match> GetMatch(int id)
        {
            var match = _matchService.GetMatchById(id);
            if (match == null)
                return NotFound();

            return Ok(match);
        }

        [HttpPost]
        public IActionResult CreateMatch([FromBody] MatchDTO matchDTO)
        {
            _matchService.CreateMatch(matchDTO);
            return CreatedAtAction(nameof(GetMatch), new { id = matchDTO.MatchId }, matchDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMatch(int id, [FromBody] MatchDTO matchDTO)
        {
            if (id != matchDTO.MatchId)
                return BadRequest();

            _matchService.UpdateMatch(id, matchDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMatch(int id)
        {
            _matchService.DeleteMatch(id);
            return NoContent();
        }
    }
}