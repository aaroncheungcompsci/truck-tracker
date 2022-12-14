using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace trucktracker.data.Entities
{
    /// <summary>
    /// Getters and setters for the TruckHistory Entity.
    /// </summary>
    public class TruckHistory
    {
        [Key]
        public Guid HistoryId {get;set;}
        public string Loc {get;set;} = string.Empty;
        public string Comments {get;set;} = string.Empty;
        public DateTime Move_In {get;set;}
        public DateTime? Move_Out {get;set;}
        public long? Total_Time {get;set;}

        // Foreign Keys, any Object that is named after themselves is not a foreign key.
        public Truck Truck {get;set;}
        public string TruckVIN {get;set;}
        public Station Station {get;set;}
        public string StationId {get;set;}
        public Person Person {get;set;}
        public Guid PersonId {get;set;}

    }
}