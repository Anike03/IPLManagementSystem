using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Models; // Add this line
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Interfaces;

namespace IPLManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamApiController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamApiController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Team>> GetTeams()
        {
            return Ok(_teamService.AllTeams);
        }

        [HttpGet("{id}")]
        public ActionResult<Team> GetTeam(int id)
        {
            var team = _teamService.GetTeamById(id);
            if (team == null)
                return NotFound();

            return Ok(team);
        }

        [HttpPost]
        public IActionResult CreateTeam([FromBody] TeamDTO teamDTO)
        {
            _teamService.CreateTeam(teamDTO);
            return CreatedAtAction(nameof(GetTeam), new { id = teamDTO.TeamId }, teamDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeam(int id, [FromBody] TeamDTO teamDTO)
        {
            if (id != teamDTO.TeamId)
                return BadRequest();

            _teamService.UpdateTeam(id, teamDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            _teamService.DeleteTeam(id);
            return NoContent();
        }
    }
}