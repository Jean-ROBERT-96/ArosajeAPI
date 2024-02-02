using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("utilisateur")]
    public class Utilisateur
    {
        [Column("id"), Key]
        public Int64 Id { get; set; }

        [Column("prenom")]
        public String Prenom { get; set; }

        [Column("nom")]
        public String Nom { get; set; }

        [Column("mail")]
        public String Mail { get; set; }

        [Column("mot_de_passe")]
        public String Password { get; set; }

        [Column("telephone")]
        public String Telephone { get; set; }

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        [Column("est_botaniste")]
        public Boolean EstBotaniste { get; set; }

        [Column("est_moderateur")]
        public Boolean EstModerateur { get; set; }
    }
}
