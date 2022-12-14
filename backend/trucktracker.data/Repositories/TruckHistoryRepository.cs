using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trucktracker.data.Entities;
using trucktracker.data.Interfaces;

namespace trucktracker.data.Repositories
{
    /// <summary>
    /// Repository for the TruckHistory entity. Handles the retrieval of data from SQL server.
    /// </summary>
    public class TruckHistoryRepository : ITruckHistoryRepository
    {
        private readonly TruckTrackerContext _truckTrackerContext;

        /// <summary>
        /// Constructor for the TruckHistoryRepository class. Initializes the context
        /// with the given parameter.
        /// </summary>
        /// <param name="truckTrackerContext">Initializes the _truckTrackerContext variable.</param>
        public TruckHistoryRepository(TruckTrackerContext truckTrackerContext)
        {
            _truckTrackerContext = truckTrackerContext;
        }

        /// <summary>
        /// Adds a TruckHistory Entity to the database.
        /// </summary>
        /// <param name="truckHistory">The entity to be added to the database.</param>
        /// <returns>The TruckHistory back to the service layer.</returns>
        public async Task<TruckHistory> AddAsync(TruckHistory truckHistory)
        {
            truckHistory.HistoryId = truckHistory.HistoryId == Guid.Empty ? Guid.NewGuid() : truckHistory.HistoryId;
            _truckTrackerContext.Add(truckHistory);
            await _truckTrackerContext.SaveChangesAsync();
            return truckHistory;
        }

        /// <summary>
        /// Finds a history entry by the Id given by the service layer.
        /// </summary>
        /// <param name="historyId">historyId to be searched</param>
        /// <returns></returns>
        public async Task<TruckHistory> FindAsync(Guid historyId)
        {
            return await _truckTrackerContext.Truck_History.FindAsync(historyId);
        }

        public IQueryable<TruckHistory> Get()
        {
            return _truckTrackerContext.Truck_History.AsQueryable();
        }

        public async Task RemoveAsync(Guid historyId)
        {
            var truckHistory = await _truckTrackerContext.Truck_History.FindAsync(historyId);
            if (truckHistory is not null)
            {
                _truckTrackerContext.Truck_History.Remove(truckHistory);
                await _truckTrackerContext.SaveChangesAsync();
            }
        }

        public async Task<TruckHistory> UpdateAsync(TruckHistory truckHistory)
        {
            var local = _truckTrackerContext.Truck_History.Local.FirstOrDefault(entity => entity.HistoryId == truckHistory.HistoryId);
            if (local is not null)
            {
                _truckTrackerContext.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _truckTrackerContext.Entry(truckHistory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _truckTrackerContext.SaveChangesAsync();
            return truckHistory;
        }
    }
}