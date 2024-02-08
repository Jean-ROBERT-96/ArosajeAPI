using Entities;
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
            var result = await _context.Annonces.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
                return null;

            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
