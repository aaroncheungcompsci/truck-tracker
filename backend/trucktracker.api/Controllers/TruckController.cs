using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using trucktracker.core.Interfaces;
using trucktracker.core.Models;

namespace trucktracker.api.Controllers
{   
    /// <summary>
    /// This controller handles the requests pertaining to
    /// the Truck table in the database
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TruckController : ControllerBase
    {
        private readonly ITruckService _truckService;
        /// <summary>
        /// Constructor for the TruckController class. Initializes 
        /// the variable _truckService.
        /// </summary>
        /// <param name="truckService">Interface of the service to be used</param>
        public TruckController(ITruckService truckService)
        {
            _truckService = truckService;
        }

        /// <summary>
        /// Gets the specified VIN number from the Truck table
        /// </summary>
        /// <param name="VIN">VIN number to be found in the table</param>
        /// <returns>Response code depending on whether the truck is found in the table</returns>
        [HttpGet("{VIN}")]
        [ActionName("GetVINAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TruckModel>> GetVINAsync(string VIN)
        {
            var truck = await _truckService.GetVINAsync(VIN);
            
            if (truck is null)
            {
                return NotFound();
            }

            return Ok(truck);
        }

        /*
        public async Task<ActionResult<TruckModel>> AddVINAsync(TruckModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<TruckModel>> DeleteVINAsync(string VIN)
        {
            throw new NotImplementedException();
        }
        */
        /// <summary>
        /// Updates a specific VIN number in the database with the properties in updateTruckModel
        /// </summary>
        /// <param name="VIN">The VIN number in the Truck table</param>
        /// <param name="updateTruckModel">The model containing the properties to update</param>
        /// <returns>HTTPS response code dependent on if a truck is successfully updated</returns>
        [HttpPut("{VIN}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<TruckModel>> UpdateVINAsync(string VIN, TruckModel updateTruckModel)
        {
            if (VIN != updateTruckModel.VIN)
            {
                return BadRequest();
            }

            var truck = await _truckService.GetVINAsync(VIN);
            if (truck is null)
            {
                return NotFound();
            }

            var truckModel = new TruckModel
            {
                VIN = updateTruckModel.VIN,
                Days_in_Offline = updateTruckModel.Days_in_Offline
            };

            var updatedTruck = await _truckService.UpdateVINAsync(truckModel);

            return NoContent();
        }

        /// <summary>
        /// Gets a list of all the VINs currently in the Truck table
        /// </summary>
        /// <returns>List of all VINs currently in database</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TruckModel>>> GetAllVINsAsync()
        {
            var trucks = await _truckService.GetAllVINsAsync();
            return Ok(trucks);
        }
    }
}