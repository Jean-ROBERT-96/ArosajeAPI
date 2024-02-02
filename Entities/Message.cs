using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("message")]
    public class Message
    {
        [Column("id"), Key]
        public Int64 Id { get; set; }

        [Column("contenu")]
        public String Contenu { get; set; }

        [Column("date_envoi")]
        public DateTime DateEnvoi { get; set; }

        [Column("conversation_id")]
        public Int64 ConversationId { get; set; }

        [Column("expedition_id")]
        public Int64 ExpediteurId { get; set; }
    }
}
