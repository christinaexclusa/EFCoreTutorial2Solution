using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreTutorial2.Models
{

    [Table("Artists")]
    public class ArtistModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public virtual ICollection<SongModel> Songs { get; set; } = null!;


    }
}
