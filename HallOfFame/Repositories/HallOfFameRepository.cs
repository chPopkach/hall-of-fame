using HallOfFame.Interfaces;
using HallOfFame.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Repositories
{
    public class HallOfFameRepository : IHallOfFameRepository
    {
        private readonly ContextDb _context;

        public HallOfFameRepository(ContextDb context)
        {
            _context = context;
        }

        public IEnumerable<Person> Get()
        {
            return _persons;
        }

        public Person Get(int id)
        {
            var person = _persons.FirstOrDefault(q => q.Id == id);

            if (person == null)
            {
                throw new Exception(message: $"Person with this ID ({id}) not found");
            }

            return person;
        }

        public void Create(PersonDTO personModelCreate)
        {
            if (personModelCreate == null)
            {
                throw new Exception(message: $"Data is filled in incorrectly");
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
                skill.PersonId = _persons.Max(q => q.Id) + 1;

                person.Skills.Add(skill);
            }

            _context.Persons.Add(person);
            _context.Skills.AddRange(person.Skills);
            Save();
        }

        public Person Delete(int id)
        {
            var skillsPerson = _context.Skills.Where(q => q.PersonId == id).ToList();
            var deletedPerson = _persons.FirstOrDefault(q => q.Id == id);

            if (skillsPerson == null || deletedPerson == null)
            {
                throw new Exception(message: $"Person with this ID ({id}) not found");
            }

            _context.Skills.RemoveRange(skillsPerson);
            _context.Persons.Remove(deletedPerson);
            Save();

            return deletedPerson;
        }

        public void Update(int id, PersonDTO personModelUpdate)
        {
            var person = _persons.FirstOrDefault(q => q.Id == id) ??
                throw new Exception(message: $"Person with this ID ({id}) not found");

            person.Name = personModelUpdate.Name;
            person.DisplayName = personModelUpdate.DisplayName;

            _context.Skills.RemoveRange(person.Skills);

            person.Skills = new List<Skill>();
            foreach (var item in personModelUpdate.Skills)
            {
                Skill skill = new Skill();
                skill.Name = item.Name;
                skill.Level = item.Level;
                skill.PersonId = _persons.Max(q => q.Id) + 1;

                person.Skills.Add(skill);
            }

            _context.Persons.Update(person);
            _context.Skills.AddRange(person.Skills);
            Save();
        }

        private List<Person> _persons => _context.Persons.Include(q => q.Skills).ToList();

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
