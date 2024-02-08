using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataContext
{
    public class DBContext : DbContext
    {
        public DbSet<JwtUsers> JwtUsers { get; set; }
        public DbSet<Annonce> Annonces { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Entretien> Entretiens { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Suivi> Suivis { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
    }
}
