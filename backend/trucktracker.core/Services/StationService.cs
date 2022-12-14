using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using trucktracker.core.Interfaces;
using trucktracker.core.Models;
using trucktracker.data.Interfaces;

namespace trucktracker.core.Services
{
    /// <summary>
    /// This service links the API layer with the data layer for the Station entity. 
    /// </summary>
    public class StationService : IStationService
    {
        private readonly IStationRepository _stationRepository;
        private readonly MapperConfiguration config;
        private readonly Mapper mapper;

        /// <summary>
        /// Constructor for the StationService class. Initializes the station repository interface with
        /// the specified parameter. (At the time of documenting, this class is not utilizing automapper
        /// yet.)
        /// </summary>
        /// <param name="stationRepository">Repository to be used to initialize the private interface.</param>
        public StationService(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        /// <summary>
        /// Retrieves a station in the repository based on the provided stationId.
        /// </summary>
        /// <param name="stationId">The stationId to look for</param>
        /// <returns>The new station model that has attributes of the searched station.</returns>
        public async Task<StationModel> GetStationAsync(string stationId)
        {
            var stationEntity = await _stationRepository.FindAsync(stationId);

            if (stationEntity is null)
            {
                return null;
            }

            return new StationModel
            {
                StationId = stationEntity.StationId,
                Num_of_Allowed_Trucks = stationEntity.Num_of_Allowed_Trucks,
                Num_of_Current_Trucks = stationEntity.Num_of_Current_Trucks
            };
        }

        /// <summary>
        /// Updates the station specified by the model attributes passed in as a parameter.
        /// </summary>
        /// <param name="model">The model used to update the station currently in the database</param>
        /// <returns>The newly updated station as a model</returns>
        public async Task<StationModel> UpdateStationAsync(StationModel model)
        {
            var stationEntity = new data.Entities.Station
            {
                StationId = model.StationId,
                Num_of_Allowed_Trucks = model.Num_of_Allowed_Trucks,
                Num_of_Current_Trucks = model.Num_of_Current_Trucks
            };

            stationEntity = await _stationRepository.UpdateAsync(stationEntity);

            return new StationModel {
                StationId = stationEntity.StationId,
                Num_of_Allowed_Trucks = stationEntity.Num_of_Allowed_Trucks,
                Num_of_Current_Trucks = stationEntity.Num_of_Current_Trucks
            };
        }

        /// <summary>
        /// Retrieves a list of all the stations currently in the database.
        /// </summary>
        /// <returns>A list of all the stations from the repository</returns>
        public async Task<List<StationModel>> GetAllStationsAsync()
        {
            IQueryable<data.Entities.Station> query = _stationRepository.Get();
            
            return await query.Select(station => new StationModel{
                StationId = station.StationId,
                Num_of_Allowed_Trucks = station.Num_of_Allowed_Trucks,
                Num_of_Current_Trucks = station.Num_of_Current_Trucks
            }).ToListAsync();
        }
    }
}