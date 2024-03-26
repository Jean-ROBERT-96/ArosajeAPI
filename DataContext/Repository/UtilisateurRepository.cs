using Entities;
using Entities.Filters;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repository
{
    public class UtilisateurRepository : IRepository<Utilisateur>
    {
        private readonly DBContext _context;

        public UtilisateurRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Utilisateur?> Delete(long id)
        {
            var result = await _context.Utilisateurs.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return null;

            _context.Utilisateurs.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Utilisateur?> Get(long id)
        {
            return await _context.Utilisateurs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Utilisateur>> Get(IFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Utilisateur>> GetAll()
        {
            return await _context.Utilisateurs.ToListAsync();
        }

        public async Task<Utilisateur?> Post(Utilisateur entity)
        {
            _context.Utilisateurs.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Utilisateur?> Put(Utilisateur entity)
        {
            var result = await _context.Utilisateurs.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
                return null;

            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
