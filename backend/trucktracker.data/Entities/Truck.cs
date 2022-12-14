using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace trucktracker.data.Entities
{
    /// <summary>
    /// Getters and setters for the Truck Entity.
    /// </summary>
    public class Truck
    {
        [Key]
        public string VIN {get;set;} = string.Empty;
        public int Days_in_Offline {get;set;}

        // Foreign Key
        public List<TruckHistory> TruckHistories {get;set;}
    }
}