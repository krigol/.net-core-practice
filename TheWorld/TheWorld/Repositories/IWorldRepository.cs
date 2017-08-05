using System.Collections.Generic;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Repositories
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAll();
        IEnumerable<Trip> GetTripsByUsername(string username);

        Trip GetTripByName(string tripName);
        Trip GetUserTripByName(string tripName, string username);

        void AddTrip(Trip trip);
        void AddStop(string tripName, Stop newStop, string username);

        Task<bool> SaveChangesAsync();
    }
}