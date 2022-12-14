using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trucktracker.core.Models;

namespace trucktracker.core.Interfaces
{
    /// <summary>
    /// Interface for the Person service
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Interface method that allows adding Person entities to the database
        /// </summary>
        /// <param name="model">Person model to be added</param>
        /// <returns></returns>
        Task<PersonModel> AddPersonAsync(PersonModel model);

        /// <summary>
        /// Interface method that allows for updating existing Person entities in the database
        /// </summary>
        /// <param name="model">Person model to be updated</param>
        /// <returns></returns>
        Task<PersonModel> UpdatePersonAsync(PersonModel model);

        /// <summary>
        /// Interface method for retrieving a Person entity from the database
        /// </summary>
        /// <param name="id">Id of person to be retrieved</param>
        /// <returns></returns>
        Task<PersonModel> GetPersonAsync(Guid id);

        /// <summary>
        /// Interface method for retrieving a list of all existing Person entities in the database
        /// </summary>
        /// <returns></returns>
        Task<List<PersonModel>> GetAllPeopleAsync();

        /// <summary>
        /// Interface method for deleting the specified Person entity
        /// </summary>
        /// <param name="id">Id of person to be deleted</param>
        /// <returns></returns>
        Task DeletePersonAsync(Guid id);
    }
}