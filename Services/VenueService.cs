using IPLManagementSystem.Data;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Interfaces;
using IPLManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IPLManagementSystem.Services
{
    public class VenueService : IVenueService
    {
        private readonly ApplicationDbContext _context;

        public VenueService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Venue> AllVenues => _context.Venues.ToList();

        public Venue? GetVenueById(int id)
        {
            return _context.Venues
                .Include(v => v.Matches)
                .FirstOrDefault(v => v.VenueId == id);
        }

        public void CreateVenue(VenueDTO venueDTO)
        {
            var venue = new Venue
            {
                Name = venueDTO.VenueName,
                Location = venueDTO.Location
            };

            _context.Venues.Add(venue);
            _context.SaveChanges();
        }

        public void UpdateVenue(int id, VenueDTO venueDTO)
        {
            var venue = _context.Venues.Find(id) ?? throw new KeyNotFoundException("Venue not found");

            venue.Name = venueDTO.VenueName;
            venue.Location = venueDTO.Location;

            _context.Venues.Update(venue);
            _context.SaveChanges();
        }

        public void DeleteVenue(int id)
        {
            var venue = _context.Venues.Find(id);
            if (venue == null)
                throw new KeyNotFoundException("Venue not found");

            _context.Venues.Remove(venue);
            _context.SaveChanges();
        }
        public IEnumerable<Venue> GetAllVenues()
        {
            return [.. _context.Venues]; // Simplified collection initialization
        }

    }
}