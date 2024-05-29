using Entities;
using Entities.Filters;
using Microsoft.EntityFrameworkCore;
using Services;

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
            PictureManager.DeletePicture(result.Image);
            return result;
        }

        public async Task<Entretien?> Get(long id)
        {
            var result = await _context.Entretiens.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
                result.Image = PictureManager.GetPicture(result.Image);

            return result;
        }

        public Task<List<Entretien>> Get(IFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Entretien>> GetAll()
        {
            var result = await _context.Entretiens.ToListAsync();
            for (int i = 0; i < result.Count; i++)
                result[i].Image = PictureManager.GetPicture(result[i].Image);

            return result;
        }

        public async Task<Entretien?> Post(Entretien entity)
        {
            entity.Image = PictureManager.SavePicture(entity.Image, $"{entity.Id}_{Guid.NewGuid()}");
            _context.Entretiens.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Entretien?> Put(Entretien entity)
        {
            var result = await _context.Entretiens.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
                return null;

            PictureManager.EditPicture(entity.Image, result.Image);
            entity.Image = result.Image;

            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
