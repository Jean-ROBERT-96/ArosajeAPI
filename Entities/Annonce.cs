using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public enum EnumEtat
    {
        Visible = 0,
        NoVisible = 1,
        Taken = 2
    }

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

        [Column("delai")]
        public Int32 Delai { get; set; }

        [Column("etat")]
        public EnumEtat Etat { get; set; }

        [Column("utilisateur_id")]
        public Int64 UtilisateurId { get; set; }
    }
}
