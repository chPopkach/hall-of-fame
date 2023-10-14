using System.ComponentModel.DataAnnotations;

namespace HallOfFame.Models
{
    public class PersonModelCreate
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string DisplayName { get; set; }

        public List<SkillModelCreate> Skills { get; set; }
    }
}
