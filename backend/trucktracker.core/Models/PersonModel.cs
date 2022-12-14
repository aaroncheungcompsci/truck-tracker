using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trucktracker.core.Models
{
    /// <summary>
    /// Getters and setters for the Person Model in the Service layer
    /// </summary>
    public class PersonModel
    {
        public Guid Id {get;set;}
        public string Email {get;set;} = string.Empty;
        public string FName {get;set;} = string.Empty;
        public string LName {get;set;} = string.Empty;
    }
}