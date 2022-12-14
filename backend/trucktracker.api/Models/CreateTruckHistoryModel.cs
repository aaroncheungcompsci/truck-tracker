using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trucktracker.data.Entities;

namespace trucktracker.api.Models
{
    /// <summary>
    /// Getters and setters for creating a new TruckHistory model
    /// </summary>
    public class CreateTruckHistoryModel
    {
        public string Loc {get;set;} = string.Empty;
        public string Comments {get;set;} = string.Empty;
        public DateTime Move_In {get;set;}
        public DateTime? Move_Out {get;set;}
        public long? Total_Time {get;set;}

        public string TruckVIN {get;set;}
        public string StationId {get;set;}
        public Guid PersonId {get;set;}
    }
}