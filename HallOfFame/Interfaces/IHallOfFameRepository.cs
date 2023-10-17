using HallOfFame.Models;

namespace HallOfFame.Interfaces
{
    public interface IHallOfFameRepository
    {
        IEnumerable<Person> Get();

        Person Get(int id);

        void Create(PersonDTO item);

        void Update(int id, PersonDTO item);

        Person Delete(int id);
    }
}
