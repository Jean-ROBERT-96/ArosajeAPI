using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public class DBContext : DbContext
    {
        public DbSet<JwtUsers> JwtUsers { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
    }
}
