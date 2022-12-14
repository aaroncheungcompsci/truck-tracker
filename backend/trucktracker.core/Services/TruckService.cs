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
    /// This service links the API layer with the data layer for the TruckHistory entity. 
    /// </summary>
    public class TruckService : ITruckService
    {
        private readonly ITruckRepository _truckRepository;

        /// <summary>
        /// This is the constructor for the TruckService class. Initializes the _truckRepository
        /// with the given repository passed as a parameter.
        /// </summary>
        /// <param name="truckRepository">The repository to be used to initialize _truckRepository.</param>
        public TruckService(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }

        /// <summary>
        /// Adds a new VIN number based on the model provided as a parameter. Automapper is only used in this method
        /// for experimentation purposes at the time. Please make it so all other functions within
        /// this layer utilizes automapper without having to declare the config and mapper variables
        /// every time.
        /// </summary>
        /// <param name="model">The VIN number to add to the database</param>
        /// <returns></returns>
        public async Task<TruckModel> AddVINAsync(TruckModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<data.Entities.Person, PersonModel>()  
            );

            var mapper = new Mapper(config);
            
            var truckEntity = new data.Entities.Truck
            {
                VIN = model.VIN,
                Days_in_Offline = model.Days_in_Offline
            };

            truckEntity = await _truckRepository.AddAsync(truckEntity);
            
            return mapper.Map<TruckModel>(truckEntity);
        }

        /// <summary>
        /// Deletes the specified VIN number from the database. Passes operation to
        /// the repository in the data layer.
        /// </summary>
        /// <param name="VIN">The VIN number to delete</param>
        public async Task DeleteVINAsync(string VIN)
        {
            await _truckRepository.RemoveAsync(VIN); //forward operation to repository
        }

        /// <summary>
        /// Retrieves a list of all the VIN numbers currently in the database.
        /// </summary>
        /// <returns>Current VIN numbers in database as a list.</returns>
        public async Task<List<TruckModel>> GetAllVINsAsync()
        {
            IQueryable<data.Entities.Truck> query = _truckRepository.Get();

            return await query.Select(truck => new TruckModel{
                VIN = truck.VIN,
                Days_in_Offline = truck.Days_in_Offline
            }).ToListAsync();
        }

        /// <summary>
        /// Retrieves a single VIN based on the specified VIN number in the parameter
        /// if it exists in the database.
        /// </summary>
        /// <param name="VIN">The VIN number to search for.</param>
        /// <returns>The VIN number and its properties in the database.</returns>
        public async Task<TruckModel> GetVINAsync(string VIN)
        {
            var truckEntity = await _truckRepository.FindAsync(VIN);

            if (truckEntity is null)
            {
                return null;
            }

            return new TruckModel
            {
                VIN = truckEntity.VIN,
                Days_in_Offline = truckEntity.Days_in_Offline
            };
        }

        /// <summary>
        /// Updates the VIN number in the database with the model provided in the parameter.
        /// </summary>
        /// <param name="model">The model with the updated properties.</param>
        /// <returns>The new TruckModel with the updated properties.</returns>
        public async Task<TruckModel> UpdateVINAsync(TruckModel model)
        {
            var truckEntity = new data.Entities.Truck
            {
                VIN = model.VIN,
                Days_in_Offline = model.Days_in_Offline
            };

            truckEntity = await _truckRepository.UpdateAsync(truckEntity);

            return new TruckModel {
                VIN = truckEntity.VIN,
                Days_in_Offline = truckEntity.Days_in_Offline
            };
        }
    }
}