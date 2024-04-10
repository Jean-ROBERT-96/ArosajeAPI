using Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DataContext.Repository
{
    public class JwtLoginRepository : IJwtConnection
    {
        private readonly DBContext _context;

        public JwtLoginRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Utilisateur?> Login(Utilisateur user)
        {
            var dbUser = await _context.Utilisateurs.FirstOrDefaultAsync(x => x.Mail.Equals(user.Mail) && x.Password.Equals(user.Password));

            if (dbUser != null)
                return dbUser;
            else
                return null;
        }

        public async Task<Utilisateur?> Register(Utilisateur user)
        {
            _context.Utilisateurs.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
