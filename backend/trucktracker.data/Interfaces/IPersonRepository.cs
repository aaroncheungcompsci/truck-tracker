using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trucktracker.data.Entities;

namespace trucktracker.data.Interfaces
{
    /// <summary>
    /// Interface for the Person Repository.
    /// </summary>
    public interface IPersonRepository
    {
        Task<Person> FindAsync (Guid id);
        Task<Person> UpdateAsync (Person person);
        Task<Person> AddAsync (Person person);
        Task RemoveAsync(Guid id);

        IQueryable<Entities.Person> Get();
    }
}