using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using trucktracker.api.Models;
using trucktracker.core.Interfaces;
using trucktracker.core.Models;
using AutoMapper;

namespace trucktracker.api.Controllers
{
    /// <summary>
    /// This controller handles the requests pertaining to the
    /// Person table in the database.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
   public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        /// <summary>
        /// Constructor for the PersonController class. Initializes the _personService
        /// private variable.
        /// </summary>
        /// <param name="personService">Interface of the service to be used</param>
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Gets the person from the Person table of the database.
        /// </summary>
        /// <param name="id">The id of the requested person</param>
        /// <returns>HTTP Response code depending on if person is found or not</returns>
        [HttpGet("{id}")]
        [ActionName("GetPersonAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonModel>> GetPersonAsync(Guid id)
        {
            var person = await _personService.GetPersonAsync(id);
            
            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        /// <summary>
        /// Creates a new row in the Person table with the given model
        /// </summary>
        /// <param name="createPersonModel">contains attributes of a person to be added to the DB</param>
        /// <returns>HTTP response code that indicates a successful creation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<PersonModel>> CreatePersonAsync(CreatePersonModel createPersonModel)
        {
            var personModel = new PersonModel
            {
                Email = createPersonModel.Email,
                FName = createPersonModel.FName,
                LName = createPersonModel.LName
            };

            var createdPerson = await _personService.AddPersonAsync(personModel);

            return CreatedAtAction(nameof(GetPersonAsync), new { Id = createdPerson.Id }, createdPerson);
        }

        /// <summary>
        /// Updates the person with the specified properties in the updatePersonModel parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatePersonModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePersonAsync(Guid id, UpdatePersonModel updatePersonModel)
        {
            if (id != updatePersonModel.Id)
            {
                return BadRequest();
            }

            var person = await _personService.GetPersonAsync(id);
            if (person is null)
            {
                return NotFound();
            }

            var personModel = new PersonModel
            {
                Id = id,
                Email = updatePersonModel.Email,
                FName = updatePersonModel.FName,
                LName = updatePersonModel.LName
            };

            var updatedPerson = await _personService.UpdatePersonAsync(personModel);

            return NoContent();
        }

        /// <summary>
        /// Deletes the specified person via id from the Person table 
        /// </summary>
        /// <param name="id">The id of the person</param>
        /// <returns>HTTP response code showing success of delete</returns>
        [HttpDelete("{id}")]
        [ActionName("GetPersonAsync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePersonAsync(Guid id)
        {
            var person = await _personService.GetPersonAsync(id);
            if (person is null)
            {
                return NotFound();
            }

            await _personService.DeletePersonAsync(id);
            return NoContent();
        }
        
        /// <summary>
        /// Obtains a list of all the current people in the Person table
        /// </summary>
        /// <returns>HTTP response code indicating success</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PersonModel>>> GetAllPeopleAsync()
        {
            var people = await _personService.GetAllPeopleAsync();
            return Ok(people);
        }
    }    
}