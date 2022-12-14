using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using trucktracker.api.Models;
using trucktracker.core.Interfaces;
using trucktracker.core.Models;

namespace trucktracker.api.Controllers
{   
    /// <summary>
    /// This controller handles all the requests pertaining to
    /// the Station table in the database
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class StationController : ControllerBase
    {
        private readonly IStationService _stationService;
        /// <summary>
        /// Constructor of the StationController class. Initializes the
        /// </summary>
        /// <param name="stationService">Interface of the service to be used</param>
        public StationController(IStationService stationService)
        {
            _stationService = stationService;
        }
        
        /// <summary>
        /// Gets a specific station from the Station table
        /// </summary>
        /// <param name="StationId">The name of the station</param>
        /// <returns>Station name and its properties</returns>
        [HttpGet("{StationId}")]
        [ActionName("GetStationAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StationModel>> GetStationAsync(string StationId)
        {
            var station = await _stationService.GetStationAsync(StationId);
            
            if (station is null)
            {
                return NotFound();
            }

            return Ok(station);
        }

        /// <summary>
        /// Updates a station with the properties specified in updateStationModel
        /// </summary>
        /// <param name="StationId">Name of the station</param>
        /// <param name="updateStationModel">Contains properties of a station</param>
        /// <returns>HTTP response code indicating a successful update</returns>
        [HttpPut("{StationId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<StationModel>> UpdateStationAsync(string StationId, UpdateStationModel updateStationModel)
        {
            if (StationId != updateStationModel.StationId)
            {
                return BadRequest();
            }

            var station = await _stationService.GetStationAsync(StationId);
            if (station is null)
            {
                return NotFound();
            }

            var stationModel = new StationModel
            {
                StationId = updateStationModel.StationId,
                Num_of_Allowed_Trucks = updateStationModel.Num_of_Allowed_Trucks,
                Num_of_Current_Trucks = updateStationModel.Num_of_Current_Trucks
            };

            var updatedStation = await _stationService.UpdateStationAsync(stationModel);

            return NoContent();
        }

        /// <summary>
        /// Gets a list of all the stations in the Station table
        /// </summary>
        /// <returns>List of all stations</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<StationModel>>> GetAllStationsAsync()
        {
            var stations = await _stationService.GetAllStationsAsync();
            return Ok(stations);
        }
    }
}