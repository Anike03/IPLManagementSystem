using IPLManagementSystem.Data;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPLManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueControllerAPI : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VenueControllerAPI(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/venue
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VenueDTO>>> GetVenues()
        {
            var venues = await _context.Venues
                .Select(v => new VenueDTO
                {
                    VenueId = v.VenueId,
                    VenueName = v.Name,
                    Location = v.Location
                })
                .ToListAsync();

            return Ok(venues);
        }

        // GET: api/venue/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VenueDTO>> GetVenue(int id)
        {
            var venue = await _context.Venues
                .FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null)
            {
                return NotFound();
            }

            var venueDTO = new VenueDTO
            {
                VenueId = venue.VenueId,
                VenueName = venue.Name,
                Location = venue.Location
            };

            return Ok(venueDTO);
        }

        // POST: api/venue
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Venue>> CreateVenue(VenueDTO venueDTO)
        {
            if (ModelState.IsValid)
            {
                var venue = new Venue
                {
                    Name = venueDTO.VenueName,
                    Location = venueDTO.Location
                };

                _context.Venues.Add(venue);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetVenue), new { id = venue.VenueId }, venue);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/venue/5
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVenue(int id, VenueDTO venueDTO)
        {
            if (id != venueDTO.VenueId)
            {
                return BadRequest();
            }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null)
            {
                return NotFound();
            }

            venue.Name = venueDTO.VenueName;
            venue.Location = venueDTO.Location;

            _context.Entry(venue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueExists(id))
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

        // DELETE: api/venue/5
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVenue(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content response
        }

        private bool VenueExists(int id)
        {
            return _context.Venues.Any(e => e.VenueId == id);
        }
    }
}
