using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using trucktracker.core.Interfaces;
using trucktracker.core.Models;
using trucktracker.data.Entities;
using trucktracker.data.Interfaces;

namespace trucktracker.core.Services
{
    /// <summary>
    /// This service links the API layer with the data layer for the TruckHistory entity. 
    /// </summary>
    public class TruckHistoryService : ITruckHistoryService
    {
        private readonly ITruckHistoryRepository _truckHistoryRepository;
        
        /// <summary>
        /// Constructor for the TruckHistoryService class. Initializes the repository 
        /// with the given parameter.
        /// </summary>
        /// <param name="truckHistoryRepository">Repository to be used to initialize _truckHistoryRepository</param>
        public TruckHistoryService(ITruckHistoryRepository truckHistoryRepository)
        {
            _truckHistoryRepository = truckHistoryRepository;
        }

        /// <summary>
        /// Delete a certain history record given the id.
        /// </summary>
        /// <param name="id">The id to search for</param>
        public async Task DeleteHistoryAsync(Guid id)
        {
            await _truckHistoryRepository.RemoveAsync(id); 
        }

        /// <summary>
        /// Retrieves a single record from the database given the specified historyId
        /// </summary>
        /// <param name="historyId">The historyId to search for</param>
        /// <returns></returns>
        public async Task<TruckHistoryModel> GetHistoryAsync(Guid historyId)
        {
            var historyEntity = await _truckHistoryRepository.FindAsync(historyId);

            if (historyEntity is null)
            {
                return null;
            }

            return new TruckHistoryModel
            {
                HistoryId = historyEntity.HistoryId,
                Loc = historyEntity.Loc,
                Comments = historyEntity.Comments,
                Move_In = historyEntity.Move_In,
                Move_Out = historyEntity.Move_Out,
                Total_Time = historyEntity.Total_Time,
                TruckVIN = historyEntity.TruckVIN,
                StationId = historyEntity.StationId,
                PersonId = historyEntity.PersonId
            };
        }

        /// <summary>
        /// Updates a history record given the model
        /// </summary>
        /// <param name="model">The model with the updated properties</param>
        /// <returns>A new model with the updated properties</returns>
        public async Task<TruckHistoryModel> UpdateHistoryAsync(TruckHistoryModel model)
        {
            var historyEntity = new data.Entities.TruckHistory
            {
                HistoryId = model.HistoryId,
                Loc = model.Loc,
                Comments = model.Comments,
                Move_In = model.Move_In,
                Move_Out = model.Move_Out,
                Total_Time = model.Total_Time,
                TruckVIN = model.TruckVIN,
                StationId = model.StationId,
                PersonId = model.PersonId
            };

            historyEntity = await _truckHistoryRepository.UpdateAsync(historyEntity);

            return new TruckHistoryModel {
                HistoryId = historyEntity.HistoryId,
                Loc = historyEntity.Loc,
                Comments = historyEntity.Comments,
                Move_In = historyEntity.Move_In,
                Move_Out = historyEntity.Move_Out,
                Total_Time = historyEntity.Total_Time,
                TruckVIN = historyEntity.TruckVIN,
                StationId = historyEntity.StationId,
                PersonId = historyEntity.PersonId
            };
        }


        /// <summary>
        /// Retrieves a list of the entire history of a specific VIN number.
        /// </summary>
        /// <param name="TruckVIN">The VIN number to search for</param>
        /// <returns>The entire history of a specific VIN as a list. Tracks by the last 3 digits.</returns>
        public async Task<List<TruckHistoryModel>> GetAllHistoryAsync(string TruckVIN)
        {
            IQueryable<data.Entities.TruckHistory> query = _truckHistoryRepository.Get();

            // this is the line that tracks how many trailing digits are tracked. If more are desired,
            // subtract a higher value dependent on how many digits should be compared.
            var VIN = TruckVIN.Substring(TruckVIN.Length - 3); 

            return await query.Select(history => new TruckHistoryModel {
                HistoryId = history.HistoryId,
                Loc = history.Loc,
                Comments = history.Comments,
                Move_In = history.Move_In,
                Move_Out = history.Move_Out,
                Total_Time = history.Total_Time,
                TruckVIN = history.TruckVIN,
                StationId = history.StationId,
                PersonId = history.PersonId
            }).Where(history => history.TruckVIN == TruckVIN ||
             history.TruckVIN.EndsWith(VIN)).ToListAsync();
        }

        /// <summary>
        /// Adds a history record with the provided model. Automapper is only used in this method
        /// for experimentation purposes at the time. Please make it so all other functions within
        /// this layer utilizes automapper without having to declare the config and mapper variables
        /// every time.
        /// </summary>
        /// <param name="model">The model to be added to the database</param>
        /// <returns>The new model with the new properties</returns>
        public async Task<TruckHistoryModel> AddHistoryAsync(TruckHistoryModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<data.Entities.TruckHistory, TruckHistoryModel>()  
            );

            var mapper = new Mapper(config);
            
            var historyEntity = new data.Entities.TruckHistory
            {
                Loc = model.Loc,
                Comments = model.Comments,
                Move_In = model.Move_In,
                Move_Out = model.Move_Out,
                Total_Time = model.Total_Time,
                TruckVIN = model.TruckVIN,
                StationId = model.StationId,
                PersonId = model.PersonId
            };

            historyEntity = await _truckHistoryRepository.AddAsync(historyEntity);
            
            return mapper.Map<TruckHistoryModel>(historyEntity);
        }
    }
}