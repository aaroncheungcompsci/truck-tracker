using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using trucktracker.api.Models;
using trucktracker.core.Interfaces;
using trucktracker.core.Models;
using trucktracker.data.Entities;
using AutoMapper;

namespace trucktracker.api.Controllers
{
    /// <summary>
    /// This controller handles the requests that pertains to
    /// the TruckHistory table in the database
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TruckHistoryController : ControllerBase
    {
        private readonly ITruckHistoryService _truckHistoryService;

        /// <summary>
        /// Constructor for the TruckHistoryController class. Initializes
        /// the 
        /// </summary>
        /// <param name="truckHistoryService"></param>
        public TruckHistoryController(ITruckHistoryService truckHistoryService)
        {
            _truckHistoryService = truckHistoryService;
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TruckHistoryModel>> CreateHistoryAsync(CreateTruckHistoryModel createHistoryModel) 
        {
            var historyModel = new TruckHistoryModel
            {
                Loc = createHistoryModel.Loc,
                Comments = createHistoryModel.Comments,
                Move_In = createHistoryModel.Move_In,
                Move_Out = createHistoryModel.Move_Out,
                Total_Time = createHistoryModel.Total_Time,
                TruckVIN = createHistoryModel.TruckVIN,
                StationId = createHistoryModel.StationId,
                PersonId = createHistoryModel.PersonId
            };

            var createdHistory = await _truckHistoryService.AddHistoryAsync(historyModel);

            return CreatedAtAction(nameof(GetAllHistoryAsync), 
                new { HistoryId = createdHistory.HistoryId, 
                      TruckVIN = createdHistory.TruckVIN,
                      StationId = createdHistory.StationId,
                      PersonId = createdHistory.PersonId }, createdHistory);
        }

        [HttpPut("{historyId}")]
        [ActionName("UpdateHistoryAsync")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<TruckHistoryModel>> UpdateHistoryAsync(Guid historyId, TruckHistoryModel updateHistoryModel) 
        {
            if (historyId != updateHistoryModel.HistoryId)
            {
                return BadRequest();
            }

            var history = await _truckHistoryService.GetHistoryAsync(historyId);
            if (history is null)
            {
                return NotFound();
            }

            var historyModel = new TruckHistoryModel
            {
                HistoryId = updateHistoryModel.HistoryId,
                Loc = updateHistoryModel.Loc,
                Comments = updateHistoryModel.Comments,
                Move_In = updateHistoryModel.Move_In,
                Move_Out = updateHistoryModel.Move_Out,
                Total_Time = updateHistoryModel.Total_Time,
                TruckVIN = updateHistoryModel.TruckVIN,
                StationId = updateHistoryModel.StationId,
                PersonId = updateHistoryModel.PersonId
            };

            var updatedHistory = await _truckHistoryService.UpdateHistoryAsync(historyModel);

            return NoContent();
        }

        [HttpGet("{TruckVIN}")]
        [ActionName("GetAllHistoryAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TruckHistoryModel>> GetAllHistoryAsync(string TruckVIN)
        {
            var history = await _truckHistoryService.GetAllHistoryAsync(TruckVIN);
            
            if (history is null)
            {
                return NotFound();
            }

            return Ok(history);
        }
    }
}