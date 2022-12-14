using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace trucktracker.data.Entities
{
    /// <summary>
    /// Getters and setters for the Person Entity.
    /// </summary>
    public class Person
    {
        public Guid Id {get;set;}
        public string Email {get;set;} = string.Empty;
        public string FName {get;set;} = string.Empty;
        public string LName {get;set;} = string.Empty;

        // Foreign Key
        public List<TruckHistory> TruckHistories {get;set;}
    }
}