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

        public async Task<List<Utilisateur>> Get(IFilter filter)
        {
            var utilisateurFilter = (UtilisateurFilter)filter;
            var query = _context.Utilisateurs.AsQueryable();

            if (utilisateurFilter.UserId.HasValue)
                query = query.Where(f => f.Id == utilisateurFilter.UserId.Value);

            if (utilisateurFilter.EstBotaniste.HasValue)
                query = query.Where(f => f.EstBotaniste == utilisateurFilter.EstBotaniste.Value);

            if (utilisateurFilter.EstModerateur.HasValue)
                query = query.Where(f => f.EstModerateur == utilisateurFilter.EstModerateur.Value);

            if (!string.IsNullOrWhiteSpace(utilisateurFilter.Nom))
                query = query.Where(f => f.Nom.Contains(utilisateurFilter.Nom, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(utilisateurFilter.Prenom))
                query = query.Where(f => f.Prenom.Contains(utilisateurFilter.Prenom, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(utilisateurFilter.Mail))
                query = query.Where(f => f.Mail.Contains(utilisateurFilter.Mail, StringComparison.OrdinalIgnoreCase));

            if (utilisateurFilter.CreeDe.HasValue)
                query = query.Where(f => f.DateCreation >= utilisateurFilter.CreeDe);

            if (utilisateurFilter.CreeA.HasValue)
                query = query.Where(f => f.DateCreation <= utilisateurFilter.CreeA);

            return await query.ToListAsync();
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
