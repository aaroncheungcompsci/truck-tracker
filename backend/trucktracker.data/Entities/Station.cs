using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trucktracker.data.Entities
{
    /// <summary>
    /// Getters and setters for the Station Entity.
    /// </summary>
    public class Station
    {
        public string StationId {get;set;} = string.Empty;
        public int Num_of_Allowed_Trucks {get;set;}
        public int Num_of_Current_Trucks {get;set;}

        // Foreign Key
        public List<TruckHistory> TruckHistories {get;set;}
    }
}