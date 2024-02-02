using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("annonce")]
    public class Annonce
    {
        [Column("id"), Key]
        public Int64 Id { get; set; }

        [Column("image")]
        public String Image { get; set; }

        [Column("title")]
        public String Title { get; set; }

        [Column("description")]
        public String Description { get; set; }

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        [Column("est_visible")]
        public Boolean EstVisible { get; set; }

        [Column("est_suspendu")]
        public Boolean EstSuspendu { get; set; }

        [Column("utilisateur_id")]
        public Int64 UtilisateurId { get; set; }
    }
}
