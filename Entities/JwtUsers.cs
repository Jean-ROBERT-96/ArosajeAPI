using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("jwt_users")]
    public class JwtUsers
    {
        [Column("id"), Key]
        public Int64 Id { get; set; }

        [Column("name")]
        public String Name { get; set; }

        [Column("password")]
        public String Password { get; set; }
    }
}
