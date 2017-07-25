using System.Collections.Generic;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Repositories
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAll();

        void AddTrip(Trip trip);
        Task<bool> SaveChangesAsync();
    }
}