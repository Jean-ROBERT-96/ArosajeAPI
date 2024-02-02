using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("entretien")]
    public class Entretien
    {
        [Column("id"), Key]
        public Int64 Id { get; set; }

        [Column("nom_plante")]
        public String NomPlante { get; set; }

        [Column("image")]
        public String Image { get; set; }

        [Column("description")]
        public String Description { get; set; }

        [Column("debut_entretien")]
        public DateTime DebutEntretien { get; set; }

        [Column("fin_entretien")]
        public DateTime FinEntretien { get; set; }

        [Column("utilisateur_id")]
        public Int64 UtilisateurId { get; set; }

        [Column("botaniste_id")]
        public Int64 BotanisteId { get; set; }
    }
}
