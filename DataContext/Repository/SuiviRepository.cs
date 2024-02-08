using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repository
{
    public class SuiviRepository : IRepository<Suivi>
    {
        private readonly DBContext _context;

        public SuiviRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Suivi?> Delete(long id)
        {
            var result = await _context.Suivis.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return null;

            _context.Suivis.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Suivi?> Get(long id)
        {
            return await _context.Suivis.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Suivi>> GetAll()
        {
            return await _context.Suivis.ToListAsync();
        }

        public async Task<Suivi?> Post(Suivi entity)
        {
            _context.Suivis.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Suivi?> Put(Suivi entity)
        {
            var result = await _context.Suivis.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
                return null;

            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
