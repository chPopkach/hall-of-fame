using System.ComponentModel.DataAnnotations;

namespace HallOfFame
{
    public class PersonDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string DisplayName { get; set; }

        public List<SkillDTO> Skills { get; set; }
    }
}
