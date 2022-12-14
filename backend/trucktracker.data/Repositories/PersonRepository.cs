using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trucktracker.data.Entities;
using trucktracker.data.Interfaces;

namespace trucktracker.data.Repositories
{
    /// <summary>
    /// Repository for the Person entity. Handles the retrieval of data from SQL server.
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        private readonly TruckTrackerContext _truckTrackerContext;

        /// <summary>
        /// Constructor for the PersonRepository class. Initializes the context with the
        /// given parameter.
        /// </summary>
        /// <param name="truckTrackerContext">The context to be used to initialize _truckTrackerContext</param>
        public PersonRepository(TruckTrackerContext truckTrackerContext)
        {
            _truckTrackerContext = truckTrackerContext;
        }

        /// <summary>
        /// Adds a person to the Database. The id is randomly generated if it does not exist, which it shouldn't
        /// when creating a new person entity that is provided by the service layer. This is to prevent third parties
        /// from manipulating the database. Changes are then saved asynchronously. 
        /// </summary>
        /// <param name="person">The person entity provided by the service layer.</param>
        /// <returns>Person object back to the service layer.</returns>
        public async Task<Person> AddAsync(Person person)
        {
            person.Id = person.Id == Guid.Empty ? Guid.NewGuid() : person.Id;
            _truckTrackerContext.Add(person);
            await _truckTrackerContext.SaveChangesAsync();
            return person;
        }

        /// <summary>
        /// Finds the specified Person by Id that is passed from the service layer.
        /// </summary>
        /// <param name="id">the Id to be found in the database</param>
        /// <returns>The Id found in the database if it exists. Returns null otherwise.</returns>
        public async Task<Person> FindAsync(Guid id)
        {
            return await _truckTrackerContext.Person.FindAsync(id);
        }

        /// <summary>
        /// Returns the queryable interface of the person entity.
        /// </summary>
        /// <returns>The Queryable interface for the Person entity</returns>
        public IQueryable<Person> Get()
        {
            return _truckTrackerContext.Person.AsQueryable();
        }

        /// <summary>
        /// Removes the Person specified by the id in the parameter.
        /// </summary>
        /// <param name="id">The id to be searched for in the database.</param>
        public async Task RemoveAsync(Guid id)
        {
            var person = await _truckTrackerContext.Person.FindAsync(id);
            if (person is not null) //removes the entity and then saves the changes to the database.
            {
                _truckTrackerContext.Person.Remove(person);
                await _truckTrackerContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Updates the person entity in the database with the person entity given by the service layer.
        /// </summary>
        /// <param name="person">The person entity to be used to update the existing record in the database.</param>
        /// <returns>the person entity back to the service layer.</returns>
        public async Task<Person> UpdateAsync(Person person)
        {
            var local = _truckTrackerContext.Person.Local.FirstOrDefault(entity => entity.Id == person.Id);
            if (local is not null)
            {
                _truckTrackerContext.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _truckTrackerContext.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _truckTrackerContext.SaveChangesAsync();
            return person;
        }
    }
}