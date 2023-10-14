using System.ComponentModel.DataAnnotations;

namespace HallOfFame.Models
{
    public class SkillModelCreate
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public byte Level { get; set; }

        [Required]
        public int PersonId { get; set; }
    }
}
