﻿using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TheWorld.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TheWorld.Repositories
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName, Stop newStop)
        {
            var trip = GetTripByName(tripName);

            if (trip != null)
            {
                trip.Stops.Add(newStop);
                _context.Stops.Add(newStop);
            }
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public IEnumerable<Trip> GetAll()
        {
            _logger.LogInformation("Getting All trips from DB");

            return _context.Trips.ToList();
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .Where(t => t.Name == tripName)
                .FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //public async Task<bool> SaveChangesAsync()
        //{
        //    return (await _context.SaveChangesAsync()) > 0;
        //}
    }
}
