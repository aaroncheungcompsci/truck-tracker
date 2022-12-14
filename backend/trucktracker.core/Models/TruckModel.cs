using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trucktracker.core.Models
{
    /// <summary>
    /// Getters and setters for the Truck model in the Services layer
    /// </summary>
    public class TruckModel
    {
        public string VIN {get;set;} = string.Empty;
        public int Days_in_Offline {get;set;}
    }
}