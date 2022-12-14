using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trucktracker.core.Models;

namespace trucktracker.core.Interfaces
{
    /// <summary>
    /// Interface for the Station service
    /// </summary>
    public interface IStationService
    {
        /// <summary>
        /// Interface method for updating an existing Station entity in the database
        /// </summary>
        /// <param name="model">model containing properties to be updated</param>
        /// <returns></returns>
        Task<StationModel> UpdateStationAsync(StationModel model);

        /// <summary>
        /// Interface method for getting a specified Station in the database
        /// </summary>
        /// <param name="StationId">StationId to be found in the database</param>
        /// <returns></returns>
        Task<StationModel> GetStationAsync(string StationId);

        /// <summary>
        /// Interface method for getting a list of all available StationIds in the database
        /// </summary>
        /// <returns></returns>
        Task<List<StationModel>> GetAllStationsAsync();
    }
}