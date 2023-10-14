using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace HallOfFame.Models
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Skill> Skills { get; set; }
    }
}
