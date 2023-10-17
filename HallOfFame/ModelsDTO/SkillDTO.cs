using System.ComponentModel.DataAnnotations;

namespace HallOfFame
{
    public class SkillDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(1, 10)]
        public byte Level { get; set; }
    }
}
