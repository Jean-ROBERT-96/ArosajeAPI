using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repository
{
    public class EntretienRepository : IRepository<Entretien>
    {
        private readonly DBContext _context;

        public EntretienRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Entretien?> Delete(long id)
        {
            var result = await _context.Entretiens.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return null;

            _context.Entretiens.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Entretien?> Get(long id)
        {
            return await _context.Entretiens.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Entretien>> GetAll()
        {
            return await _context.Entretiens.ToListAsync();
        }

        public async Task<Entretien?> Post(Entretien entity)
        {
            _context.Entretiens.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Entretien?> Put(Entretien entity)
        {
            var result = await _context.Entretiens.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
                return null;

            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
