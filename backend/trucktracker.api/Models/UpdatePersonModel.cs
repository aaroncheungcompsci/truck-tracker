using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trucktracker.api.Models
{
    /// <summary>
    /// Getters and setters for updating an existing Person model
    /// </summary>
    public class UpdatePersonModel
    {
        public Guid Id {get;set;}
        public string Email {get;set;} = string.Empty;
        public string FName {get;set;} = string.Empty;
        public string LName {get;set;} = string.Empty;
    }
}