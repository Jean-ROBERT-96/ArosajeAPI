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

        public async Task<JwtUsers?> Login(JwtUsers user)
        {
            string pass = string.Empty;
            using (var hash = SHA256.Create())
            {
                var bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                foreach (var b in bytes)
                    pass += $"{b:X2}";
            }

            var dbUser = await _context.JwtUsers.FirstOrDefaultAsync(x => x.Name.Equals(user.Name) && x.Password.Equals(user.Password));

            if (dbUser != null)
                return dbUser;
            else
                return null;
        }
    }
}
