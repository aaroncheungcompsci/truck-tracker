using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using trucktracker.core.Models;
using trucktracker.core.Interfaces;
using trucktracker.data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace trucktracker.core.Services
{
    /// <summary>
    /// This service links the API layer with the data layer for the Person entity. 
    /// </summary>
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly MapperConfiguration config;
        private readonly Mapper mapper;

        /// <summary>
        /// Constructor for the PersonService class. Initializes the mapper (to be used in other
        /// classes somehow. The proper way to do it is to not initialize the mapper in each class,
        /// but to actually have it be at the "top most level" of the application so it does not
        /// need to be referenced in separate instances. Documentation for Automapper on how to
        /// do this is available online.)
        /// </summary>
        /// <param name="personRepository">Initializer for the _personRepository variable</param>
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<data.Entities.Person, PersonModel>()  
            );
            mapper = new Mapper(config);
        }

        /// <summary>
        /// Maps the model that is passed in into the repository located in the data layer.
        /// Before that new Person Model is returned, the Add Async function in the data layer
        /// is invoked.
        /// </summary>
        /// <param name="model">Model passed in from API layer to be mapped</param>
        /// <returns>Person model mapped from personEntity using auto mapper</returns>
        public async Task<PersonModel> AddPersonAsync(PersonModel model) 
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            
            var personEntity = new data.Entities.Person
            {
                Email = model.Email,
                FName = model.FName,
                LName = model.LName
            };

            personEntity = await _personRepository.AddAsync(personEntity);
            
            return mapper.Map<PersonModel>(personEntity);
        }
        /// <summary>
        /// Deletes the person in the database by searching the id. Operation is passed
        /// to the repository in order to do so.
        /// </summary>
        /// <param name="id">id passed from API layer to be passed into repository</param>
        public async Task DeletePersonAsync(Guid id)
        {
            await _personRepository.RemoveAsync(id); //forward operation to repository
        }

        /// <summary>
        /// Retrieves the person by id from the repository. If null, returns null.
        /// </summary>
        /// <param name="id">The id of the person to be searched from the API layer.</param>
        /// <returns>A new Person model mapped from the entity retrieved from the repository.</returns>
        public async Task<PersonModel> GetPersonAsync(Guid id)
        {
            var personEntity = await _personRepository.FindAsync(id);

            if (personEntity is null)
            {
                return null;
            }

            return mapper.Map<PersonModel>(personEntity);
        }

        /// <summary>
        /// Updates the person with the model passed in from the API layer.
        /// </summary>
        /// <param name="model">The model to be used to update the existing data</param>
        /// <returns>New person model mapped from the personEntity variable.</returns>
        public async Task<PersonModel> UpdatePersonAsync(PersonModel model)
        {
            var personEntity = new data.Entities.Person
            {
                Id = model.Id,
                Email = model.Email,
                FName = model.FName,
                LName = model.LName
            };

            personEntity = await _personRepository.UpdateAsync(personEntity);

            return mapper.Map<PersonModel>(personEntity);
        }

        /// <summary>
        /// Retrieves a list of all the people currently in the database through the repository.
        /// </summary>
        /// <returns>A list of all the people in the database.</returns>
        public async Task<List<PersonModel>> GetAllPeopleAsync()
        {
            IQueryable<data.Entities.Person> query = _personRepository.Get();
            
            return await query.Select(person => new PersonModel{
                Id = person.Id,
                Email = person.Email,
                FName = person.FName,
                LName = person.LName
            }).ToListAsync();
        }
    }
}