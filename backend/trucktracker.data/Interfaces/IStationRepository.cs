using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trucktracker.data.Interfaces
{
    /// <summary>
    /// Interface for the Station Repository.
    /// </summary>
    public interface IStationRepository
    {
        Task<Entities.Station> FindAsync (string StationId);
        Task<Entities.Station> UpdateAsync (Entities.Station station);
        Task RemoveAsync(string StationId);
        IQueryable<Entities.Station> Get();
    }
}