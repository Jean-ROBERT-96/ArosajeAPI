using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("suivi")]
    public class Suivi
    {
        [Column("id"), Key]
        public Int64 Id { get; set; }

        [Column("image")]
        public String Image { get; set; }

        [Column("remarque")]
        public String Remarque { get; set; }

        [Column("date_entretien")]
        public DateTime DateEntretien { get; set; }

        [Column("entretien_id")]
        public Int64 EntretienId { get; set; }
    }
}
