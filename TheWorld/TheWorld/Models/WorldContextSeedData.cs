using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldContextSeedData
    {
        private WorldContext _context;

        public WorldContextSeedData(WorldContext context)
        {
            _context = context;
        }

        public async Task EnsureSeedData()
        {
            if (!_context.Trips.Any())
            {
                var usTrip = new Trip
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "Us Trip",
                    UserName = "", //TODO Add Username
                    Stops = new List<Stop>()
                    {
                        new Stop() { Name = "Atlanta", Arival = new DateTime(2014, 6, 4), Latitude = 12.32, Longitude = 32.21, Order = 1},
                        new Stop() { Name = "New York", Arival = new DateTime(2014, 6, 5), Latitude = 13.32, Longitude = 31.21, Order = 2},
                        new Stop() { Name = "Atlanta", Arival = new DateTime(2014, 6, 6), Latitude = 14.32, Longitude = 30.21, Order = 3}
                    }
                };

                _context.Trips.Add(usTrip);

                _context.Stops.AddRange(usTrip.Stops);

                var worldTrip = new Trip
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "World Trip",
                    UserName = "", //TODO Add Username
                    Stops = new List<Stop>()
                    {
                        new Stop() { Name = "Greece", Arival = new DateTime(2014, 6, 4), Latitude = 22, Longitude = 44, Order = 1},
                        new Stop() { Name = "Italy", Arival = new DateTime(2014, 6, 5), Latitude = 33, Longitude = 88, Order = 2},
                        new Stop() { Name = "Japan", Arival = new DateTime(2014, 6, 6), Latitude = 44, Longitude = 77, Order = 3}
                    }
                };

                _context.Trips.Add(worldTrip);

                _context.Stops.AddRange(worldTrip.Stops);

                await _context.SaveChangesAsync();
            }
        }
    }
}
