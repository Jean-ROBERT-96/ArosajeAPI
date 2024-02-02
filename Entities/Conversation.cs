using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("conversation")]
    public class Conversation
    {
        [Column("id"), Key]
        public Int64 Id { get; set; }

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        [Column("expediteur_id")]
        public Int64 ExpediteurId { get; set; }

        [Column("destinataire_id")]
        public Int64 DestinataireId { get; set; }
    }
}
