using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Models; // Add this line
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Interfaces;

namespace IPLManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueApiController : ControllerBase
    {
        private readonly IVenueService _venueService;

        public VenueApiController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Venue>> GetVenues()
        {
            return Ok(_venueService.AllVenues);
        }

        [HttpGet("{id}")]
        public ActionResult<Venue> GetVenue(int id)
        {
            var venue = _venueService.GetVenueById(id);
            if (venue == null)
                return NotFound();

            return Ok(venue);
        }

        [HttpPost]
        public IActionResult CreateVenue([FromBody] VenueDTO venueDTO)
        {
            _venueService.CreateVenue(venueDTO);
            return CreatedAtAction(nameof(GetVenue), new { id = venueDTO.VenueId }, venueDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVenue(int id, [FromBody] VenueDTO venueDTO)
        {
            if (id != venueDTO.VenueId)
                return BadRequest();

            _venueService.UpdateVenue(id, venueDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVenue(int id)
        {
            _venueService.DeleteVenue(id);
            return NoContent();
        }
    }
}