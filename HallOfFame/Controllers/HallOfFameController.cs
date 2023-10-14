using HallOfFame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.CompilerServices;

namespace HallOfFame.Controllers
{
    [Route("[controller]/api/v1/")]
    public class HallOfFameController : Controller
    {
        private readonly ContextDb _context;

        public HallOfFameController(ContextDb context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Person> Get() => _context.Persons.Include(q => q.Skills).ToList();

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _context.Persons.Include(q => q.Skills).FirstOrDefault(q => q.Id == id);

            if (person == null) return NotFound();

            return Ok(person);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var skillsPerson = _context.Skills.Where(q => q.PersonId == id).ToList();
            var deletedPerson = _context.Persons.FirstOrDefault(q => q.Id == id);

            if (skillsPerson == null || deletedPerson == null) return NotFound();

            foreach (var item in skillsPerson)
            {
                _context.Skills.Remove(item);
            }

            _context.Persons.Remove(deletedPerson);
            _context.SaveChanges();

            return Ok(deletedPerson);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonModelCreate personModelCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = new Person 
            { 
                Name = personModelCreate.Name,
                DisplayName = personModelCreate.DisplayName,
                Skills = new List<Skill>()
            };

            foreach (var item in personModelCreate.Skills)
            {
                Skill skill = new Skill();
                skill.Name = item.Name;
                skill.Level = item.Level;
                skill.PersonId = _context.Persons.Max(q => q.Id) + 1;

                person.Skills.Add(skill);
            }

            _context.Persons.Add(person);
            _context.Skills.AddRange(person.Skills);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), person);
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] PersonModelCreate personModelUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = _context.Persons.Include(q => q.Skills).FirstOrDefault(q => q.Id == id);

            person.Name = personModelUpdate.Name;
            person.DisplayName = personModelUpdate.DisplayName;

            _context.Skills.RemoveRange(person.Skills);

            person.Skills = new List<Skill>();
            foreach (var item in personModelUpdate.Skills)
            {
                Skill skill = new Skill();
                skill.Name = item.Name;
                skill.Level = item.Level;
                skill.PersonId = _context.Persons.Max(q => q.Id) + 1;

                person.Skills.Add(skill);
            }

            _context.Persons.Update(person);
            _context.Skills.AddRange(person.Skills);
            _context.SaveChanges();

            return Ok(personModelUpdate);
        }
    }
}
