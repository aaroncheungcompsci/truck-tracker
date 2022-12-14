using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trucktracker.data.Entities;
using trucktracker.data.Interfaces;

namespace trucktracker.data.Repositories
{
    /// <summary>
    /// Repository for the Truck entity. Handles the retrieval of data from SQL server.
    /// </summary>
    public class TruckRepository : ITruckRepository
    {
        private readonly TruckTrackerContext _truckTrackerContext;

        /// <summary>
        /// Constructor for the TruckRepository class. Initializes the context
        /// with the provided parameter.
        /// </summary>
        /// <param name="truckTrackerContext"></param>
        public TruckRepository(TruckTrackerContext truckTrackerContext)
        {
            _truckTrackerContext = truckTrackerContext;
        }

        /// <summary>
        /// To be Implemented.
        /// </summary>
        /// <param name="truck"></param>
        /// <returns></returns>
        public Task<Truck> AddAsync(Truck truck)
        {
            // should be implemented as soon as access to MES database is approved.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds a VIN number specified by the parameter.
        /// </summary>
        /// <param name="VIN">VIN number to find</param>
        public async Task<Truck> FindAsync(string VIN)
        {
            return await _truckTrackerContext.Trucks.FindAsync(VIN);
        }

        /// <summary>
        /// Returns the queryable interface
        /// </summary>
        /// <returns>the queryable interface</returns>
        public IQueryable<Truck> Get()
        {
            return _truckTrackerContext.Trucks.AsQueryable();
        }

        /// <summary>
        /// Removes the specified VIN number from the database.
        /// </summary>
        /// <param name="VIN">The VIN number to be deleted.</param>
        public async Task RemoveAsync(string VIN)
        {
            var truck = await _truckTrackerContext.Trucks.FindAsync(VIN);
            if (truck is not null)
            {
                _truckTrackerContext.Trucks.Remove(truck);
                await _truckTrackerContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Updates the truck specified in the parameter.
        /// </summary>
        /// <param name="truck">The truck to update.</param>
        /// <returns>The truck parameter once everything is updated in the database.</returns>
        public async Task<Truck> UpdateAsync(Truck truck)
        {
            var local = _truckTrackerContext.Trucks.Local.FirstOrDefault(entity => entity.VIN == truck.VIN);
            if (local is not null)
            {
                _truckTrackerContext.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _truckTrackerContext.Entry(truck).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _truckTrackerContext.SaveChangesAsync();
            return truck;
        }
    }
}