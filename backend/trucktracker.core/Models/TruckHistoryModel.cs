using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trucktracker.data.Entities;

namespace trucktracker.core.Models
{
    /// <summary>
    /// Getters and setters for the Truck History model in the Services layer
    /// </summary>
    public class TruckHistoryModel
    {
        public Guid HistoryId {get;set;}
        public string Loc {get;set;}  = string.Empty;
        public string Comments {get;set;} = string.Empty;// ? means nullable
        public DateTime Move_In {get;set;}
        public DateTime? Move_Out {get;set;}
        public long? Total_Time {get;set;}

        public string TruckVIN {get;set;}
        public string StationId {get;set;}
        public Guid PersonId {get;set;}
    }
}