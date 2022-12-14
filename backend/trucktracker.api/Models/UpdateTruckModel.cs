using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trucktracker.api.Models
{
    /// <summary>
    /// Getters and setters for updating an existing Truck model
    /// </summary>
    public class UpdateTruckModel
    {
        public string VIN {get;set;} = string.Empty;
        public int Days_in_Offline {get;set;}
    }
}