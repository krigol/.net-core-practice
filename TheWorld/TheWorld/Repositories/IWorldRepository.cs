using System.Collections.Generic;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Repositories
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAll();

        Trip GetTripByName(string tripName);

        void AddTrip(Trip trip);
        void AddStop(string tripName, Stop newStop);

        Task<bool> SaveChangesAsync();
    }
}