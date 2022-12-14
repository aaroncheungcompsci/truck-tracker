using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trucktracker.data.Entities;
using trucktracker.data.Interfaces;

namespace trucktracker.data.Repositories
{
    /// <summary>
    /// Repository for the Station entity. Handles the retrieval of data from SQL server.
    /// </summary>
    public class StationRepository : IStationRepository
    {
        private readonly TruckTrackerContext _truckTrackerContext;

        /// <summary>
        /// Constructor for the StationRepository class. Initializes the context with the parameter
        /// that is given.
        /// </summary>
        /// <param name="truckTrackerContext"></param>
        public StationRepository(TruckTrackerContext truckTrackerContext)
        {
            _truckTrackerContext = truckTrackerContext;
        }

        /// <summary>
        /// Retrieves the stationId specified by the parameter.
        /// </summary>
        /// <param name="StationId">The stationId to look for</param>
        /// <returns>The async function with the specified parameter, StationId.</returns>
        public async Task<Station> FindAsync(string StationId)
        {
            return await _truckTrackerContext.Stations.FindAsync(StationId);
        }

        /// <summary>
        /// Retrieves the interface for the service layer.
        /// </summary>
        /// <returns>The IQueryable interface for the Station entity.</returns>
        public IQueryable<Station> Get()
        {
            return _truckTrackerContext.Stations.AsQueryable();
        }

        /// <summary>
        /// Removes the specified StationId from the database.
        /// Not implemented because it was not needed at the time.
        /// </summary>
        /// <param name="StationId">StationId to be deleted.</param>
        /// <returns>Nothing for now.</returns>
        public Task RemoveAsync(string StationId)
        {
            //may not even need to implement this, unsure as of now
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the station that is currently in the database with the properties specified in the station
        /// entity that was passed in from the service layer.
        /// </summary>
        /// <param name="station">Entity to be used to update the station in the database.</param>
        /// <returns>The station back to the service layer after the database has been updated.</returns>
        public async Task<Station> UpdateAsync(Station station)
        {
            var local = _truckTrackerContext.Stations.Local.FirstOrDefault(entity => entity.StationId == station.StationId);
            if (local is not null)
            {
                _truckTrackerContext.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _truckTrackerContext.Entry(station).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _truckTrackerContext.SaveChangesAsync();
            return station;
        }
    }
}