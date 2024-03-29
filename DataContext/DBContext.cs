﻿using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataContext
{
    public class DBContext : DbContext
    {
        public DbSet<Annonce> Annonces { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Entretien> Entretiens { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Suivi> Suivis { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
