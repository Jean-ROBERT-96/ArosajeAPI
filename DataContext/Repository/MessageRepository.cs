using Entities;
using Entities.Filters;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repository
{
    public class MessageRepository : IRepository<Message>
    {
        private readonly DBContext _context;

        public MessageRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Message?> Delete(long id)
        {
            var result = await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return null;

            _context.Messages.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Message?> Get(long id)
        {
            return await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Message>> Get(IFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Message>> GetAll()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Message?> Post(Message entity)
        {
            _context.Messages.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Message?> Put(Message entity)
        {
            var result = await _context.Messages.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
                return null;

            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
