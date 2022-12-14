using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trucktracker.data.Entities;

namespace trucktracker.data.Interfaces
{
    /// <summary>
    /// Interface for the Truck History Repository.
    /// </summary>
    public interface ITruckHistoryRepository
    {
        Task<Entities.TruckHistory> FindAsync (Guid historyId);
        Task<Entities.TruckHistory> UpdateAsync (Entities.TruckHistory truck);
        Task<Entities.TruckHistory> AddAsync (Entities.TruckHistory truck);
        Task RemoveAsync(Guid historyId);

        IQueryable<Entities.TruckHistory> Get();
    }
}