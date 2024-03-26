using Entities;
using Entities.Filters;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repository
{
    public class ConversationRepository : IRepository<Conversation>
    {
        private readonly DBContext _context;

        public ConversationRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Conversation?> Delete(long id)
        {
            var result = await _context.Conversations.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return null;

            _context.Conversations.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Conversation?> Get(long id)
        {
            return await _context.Conversations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Conversation>> Get(IFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Conversation>> GetAll()
        {
            return await _context.Conversations.ToListAsync();
        }

        public async Task<Conversation?> Post(Conversation entity)
        {
            _context.Conversations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Conversation?> Put(Conversation entity)
        {
            var result = await _context.Conversations.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
                return null;

            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
