using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreTutorial2.Models
{
    [Table("Songs")]
    public class SongModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }

        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        public virtual ArtistModel? Artist { get; set; }

        public virtual GenreModel? Genre { get; set; }
    }
}
