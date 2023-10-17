using HallOfFame.Interfaces;
using HallOfFame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.CompilerServices;

namespace HallOfFame.Controllers
{
    [Route("api/v1/persons")]
    public class HallOfFameController : Controller
    {
        IHallOfFameRepository repository;

        public HallOfFameController(IHallOfFameRepository hallOfFameRepository)
        {
            repository = hallOfFameRepository;
        }

        [HttpGet(Name = "GetAllPersons")]
        public IEnumerable<Person> Get() => repository.Get();

        [HttpGet("{id}", Name = "GetPerson")]
        public IActionResult Get(int id)
        {
            try
            {
                var person = repository.Get(id);

                return Ok(person);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                repository.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonDTO personModelCreate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (personModelCreate == null) return BadRequest();

            try
            {
                repository.Create(personModelCreate);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([Required] int id, [FromBody] PersonDTO personModelUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (personModelUpdate == null) return BadRequest();

            try
            {
                repository.Update(id, personModelUpdate);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
