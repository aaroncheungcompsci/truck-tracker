using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trucktracker.core.Models
{
    /// <summary>
    /// Getters and setters for the Station model in the Services layer
    /// </summary>
    public class StationModel
    {
        public string StationId {get;set;} = string.Empty;
        public int Num_of_Allowed_Trucks {get;set;}
        public int Num_of_Current_Trucks {get;set;}
    }
}