using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using trucktracker.data.Entities;

namespace trucktracker.data
{
    /// <summary>
    /// Context class required to ensure relationship to database. This houses
    /// all the tables that are present within the database.
    /// </summary>
    public class TruckTrackerContext : DbContext
    {
        public TruckTrackerContext(DbContextOptions<TruckTrackerContext> options)
        : base(options)
        {

        }

        public DbSet<Entities.Truck> Trucks {get;set;}
        public DbSet<Entities.Station> Stations {get;set;}
        public DbSet<Entities.Person> Person {get;set;}
        public DbSet<Entities.TruckHistory> Truck_History {get;set;}

        
    }
}