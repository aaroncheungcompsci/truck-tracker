using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trucktracker.api.Models
{
    /// <summary>
    /// Getters and setters for creating a new Person model
    /// </summary>
    public class CreatePersonModel
    {
        public string Email {get;set;} = string.Empty;
        public string FName {get;set;} = string.Empty;
        public string LName {get;set;} = string.Empty;
    }
}