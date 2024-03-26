using Entities;
using Entities.Filters;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repository
{
    public class AnnonceRepository : IRepository<Annonce>
    {
        private readonly DBContext _context;

        public AnnonceRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Annonce?> Delete(long id)
        {
            var result = await _context.Annonces.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return null;

            _context.Annonces.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Annonce?> Get(long id)
        {
            return await _context.Annonces.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Annonce>> Get(IFilter filter)
        {
            var annonceFilter = (AnnonceFilter)filter;
            var query = _context.Annonces.AsQueryable();

            if (annonceFilter.UserId.HasValue)
                query = query.Where(f => f.UtilisateurId == annonceFilter.UserId.Value);

            if (annonceFilter.Etat.HasValue)
                query = query.Where(f => f.Etat == annonceFilter.Etat.Value);

            if (!string.IsNullOrWhiteSpace(annonceFilter.Title))
                query = query.Where(f => f.Title.Contains(annonceFilter.Title, StringComparison.OrdinalIgnoreCase));

            if (annonceFilter.PosteDe != null)
                query = query.Where(f => f.DateCreation >= annonceFilter.PosteDe);

            if (annonceFilter.PosteA != null)
                query = query.Where(f => f.DateCreation <= annonceFilter.PosteA);

            return await query.ToListAsync();
        }

        public async Task<List<Annonce>> GetAll()
        {
            return await _context.Annonces.ToListAsync();
        }

        public async Task<Annonce?> Post(Annonce entity)
        {
            _context.Annonces.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Annonce?> Put(Annonce entity)
        {
            var tt = new Annonce();
            var result = await _context.Annonces.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
                return null;

            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
