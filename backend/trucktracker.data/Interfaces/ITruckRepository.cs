using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trucktracker.data.Interfaces
{
    /// <summary>
    /// Interface for the Truck repository.
    /// </summary>
    public interface ITruckRepository
    {
        Task<Entities.Truck> FindAsync (string VIN);
        Task<Entities.Truck> UpdateAsync (Entities.Truck truck);
        Task<Entities.Truck> AddAsync (Entities.Truck truck);
        Task RemoveAsync(string VIN);

        IQueryable<Entities.Truck> Get();
    }
}