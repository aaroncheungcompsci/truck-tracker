using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trucktracker.core.Models;

namespace trucktracker.core.Interfaces
{
    /// <summary>
    /// Interface for the Truck service
    /// </summary>
    public interface ITruckService
    {
        /// <summary>
        /// Interface method that gets the specified VIN number
        /// </summary>
        /// <param name="VIN">VIN number to be retrieved</param>
        /// <returns></returns>
        Task<TruckModel> GetVINAsync(string VIN);

        /// <summary>
        /// Interface method that adds a VIN number to the database
        /// </summary>
        /// <param name="model">Model to be added to the database</param>
        /// <returns></returns>
        Task<TruckModel> AddVINAsync(TruckModel model);

        /// <summary>
        /// Interface method that deletes a VIN number from the database
        /// </summary>
        /// <param name="VIN"> VIN number to be deleted</param>
        /// <returns></returns>
        Task DeleteVINAsync(string VIN);

        /// <summary>
        /// Interface method that updates the VIN number specified within the model (should just be a string
        /// of the VIN number instead)
        /// </summary>
        /// <param name="model">Model to be updated (should just be a string)</param>
        /// <returns></returns>
        Task<TruckModel> UpdateVINAsync(TruckModel model);
        
        /// <summary>
        /// Interface method that retrieves a list of all the VINs currently in the database
        /// </summary>
        /// <returns></returns>
        Task<List<TruckModel>> GetAllVINsAsync();
    }
}