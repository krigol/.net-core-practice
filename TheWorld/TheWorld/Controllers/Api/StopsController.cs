﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Repositories;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    [Authorize]
    public class StopsController : Controller
    {
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;
        private GeoCoordsService _coordsService;

        public StopsController(IWorldRepository repository,
            ILogger<StopsController> logger,
            GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGet]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetUserTripByName(tripName, User.Identity.Name);

                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get stops {ex}");
            }

            return BadRequest("Failed to get stops");
        }

        [HttpPost]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(vm);
                    var result = await _coordsService.GetCoordsAsync(newStop.Name);
                    if (!result.Success)
                    {
                        _logger.LogError(result.Message);
                    }
                    else
                    {
                        newStop.Latitude = (double)result.Latitude;
                        newStop.Longitude = (double)result.Longitude;

                        _repository.AddStop(tripName, newStop, User.Identity.Name);

                        if (await _repository.SaveChangesAsync())
                        {
                            return Created($"api/trips/{tripName}/stops/{newStop.Name}",
                                Mapper.Map<StopViewModel>(newStop));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new stop: {ex}");
            }

            return BadRequest("Failed to save new stop");
        } 
    }
}
