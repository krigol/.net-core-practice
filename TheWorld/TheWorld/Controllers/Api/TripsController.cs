using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Repositories;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;
        private Microsoft.Extensions.Logging.ILogger<TripsController> _logger;

        public TripsController(IWorldRepository repository, Microsoft.Extensions.Logging.ILogger<TripsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAll();
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Trips: {ex}");

                return BadRequest("Error occurred");
            }
        }   
        
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(theTrip);
                _repository.AddTrip(newTrip);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
                else
                {
                    return BadRequest("Failed to save changes to the data base");
                }
            }

            return BadRequest();
        }
    }
}
