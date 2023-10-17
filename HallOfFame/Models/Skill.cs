using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HallOfFame.Models
{
    public class Skill
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(1, 10)]
        public byte Level { get; set; }

        [Required]
        public int PersonId { get; set; }

        public virtual Person? Person { get; set; } = null;
    }
}
