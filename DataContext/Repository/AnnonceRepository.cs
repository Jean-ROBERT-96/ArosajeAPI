using Entities;
using Entities.Filters;
using Microsoft.EntityFrameworkCore;
using Services;

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
            PictureManager.DeletePicture(result.Image);
            return result;
        }

        public async Task<Annonce?> Get(long id)
        {
            var result = await _context.Annonces.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
                result.Image = PictureManager.GetPicture(result.Image);

            return result;
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

            if (annonceFilter.PosteDe.HasValue)
                query = query.Where(f => f.DateCreation >= annonceFilter.PosteDe);

            if (annonceFilter.PosteA.HasValue)
                query = query.Where(f => f.DateCreation <= annonceFilter.PosteA);

            var result = await query.ToListAsync();
            for(int i = 0; i < result.Count; i++)
                result[i].Image = PictureManager.GetPicture(result[i].Image);

            return result;
        }

        public async Task<List<Annonce>> GetAll()
        {
            var result = await _context.Annonces.ToListAsync();
            for (int i = 0; i < result.Count; i++)
                result[i].Image = PictureManager.GetPicture(result[i].Image);

            return result;
        }

        public async Task<Annonce?> Post(Annonce entity)
        {
            entity.Image = PictureManager.SavePicture(entity.Image, $"{entity.Id}_{DateTime.UtcNow:dd-MM-yyyy}");
            _context.Annonces.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Annonce?> Put(Annonce entity)
        {
            var result = await _context.Annonces.FirstOrDefaultAsync(x => x.Id == entity.Id);
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
