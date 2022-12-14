using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trucktracker.api.Models
{
    /// <summary>
    /// Getters and setters for updating an existing Station model
    /// </summary>
    public class UpdateStationModel
    {
        public string StationId {get;set;} = string.Empty;
        public int Num_of_Allowed_Trucks {get;set;}
        public int Num_of_Current_Trucks {get;set;}
    }
}