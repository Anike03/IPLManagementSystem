using IPLManagementSystem.DTOs;
using IPLManagementSystem.Models;
using System.Collections.Generic;

namespace IPLManagementSystem.Interfaces
{
    public interface IVenueService
    {
        IEnumerable<Venue> AllVenues { get; }

        Venue? GetVenueById(int id); // Add nullable return type
        void CreateVenue(VenueDTO venueDTO);
        void UpdateVenue(int id, VenueDTO venueDTO);
        void DeleteVenue(int id);
    }
}