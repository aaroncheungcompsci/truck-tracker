using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace trucktracker.data
{
    /// <summary>
    /// Context Factory class required to ensure relationship to database.
    /// </summary>
    public class TruckTrackerContextFactory : IDesignTimeDbContextFactory<TruckTrackerContext>
    {
        public TruckTrackerContext CreateDbContext(string[] args)
        {
            
            var optionsBuilder = new DbContextOptionsBuilder<TruckTrackerContext>();
            // Replace string with the appropriate connection string to a specific SQL server database.
            //Server=NMC-HQ-3038\\MSSQLSERVER01;Database=TruckTracker;Trusted_Connection=True;
            optionsBuilder.UseSqlServer("Server=NMC-HQ-3038\\MSSQLSERVER01;Database=TruckTracker;Trusted_Connection=True;");

            return new TruckTrackerContext(optionsBuilder.Options);
        }
    }
}