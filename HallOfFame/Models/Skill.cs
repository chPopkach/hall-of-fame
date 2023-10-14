using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HallOfFame.Models
{
    public class Skill
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public byte Level { get; set; }

        [Required]
        public int PersonId { get; set; }

        public Person Person { get; set; }
    }
}
