using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trucktracker.core.Models;
using trucktracker.data.Entities;

namespace trucktracker.core.Interfaces
{
    /// <summary>
    /// Interface for the TruckHistory service
    /// </summary>
    public interface ITruckHistoryService
    {
        /// <summary>
        /// Interface method for adding a new record for a VIN number in the database
        /// </summary>
        /// <param name="model">Model containing data to be added as a record in the database</param>
        /// <returns></returns>
        Task<TruckHistoryModel> AddHistoryAsync(TruckHistoryModel model);

        /// <summary>
        /// Interface method for updating a specific record in the database
        /// </summary>
        /// <param name="model">Model containing data to be added to an existing record in the database</param>
        /// <returns></returns>
        Task<TruckHistoryModel> UpdateHistoryAsync(TruckHistoryModel model);

        /// <summary>
        /// Interface method for getting a single record from the database
        /// </summary>
        /// <param name="historyId">Id to be searched</param>
        /// <returns></returns>
        Task<TruckHistoryModel> GetHistoryAsync(Guid historyId);

        /// <summary>
        /// Interface method for deleting a specific record in the database
        /// </summary>
        /// <param name="historyId">Id to be deleted</param>
        /// <returns></returns>
        Task DeleteHistoryAsync(Guid historyId);

        /// <summary>
        /// Interface method that retrieves a list of all the records belonging to a specific VIN number
        /// </summary>
        /// <param name="TruckVIN">VIN number to be searched</param>
        /// <returns></returns>
        Task<List<TruckHistoryModel>> GetAllHistoryAsync(string TruckVIN); //gets all history of a specific VIN
    }
}