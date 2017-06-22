using EmailManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Data.Repository
{
    public class EnEventRepository : Repo<EnEvent>, IEnEventRepository
    {
        public EnEventRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<EnEvent> GetEventsByNameAsync(string name)
        {
            return await _context.EnEvents.FirstOrDefaultAsync(n => n.Name.Equals(name));
        }

        public async Task<EnEvent> GetEventsById(int id)
        {
            return await _context.EnEvents.FirstOrDefaultAsync(n => n.Id.Equals(id));
        }

        public async Task<IEnumerable<EnEvent>> GetAllEvents()
        {
            return await _context.EnEvents.OrderBy(r => r.Name).ToListAsync();
        }

        public void AddEvent(EnEvent enEvent)
        {
            _context.EnEvents.Add(enEvent);
        }

        public void DeleteEvent(EnEvent enEvent)
        {
            _context.EnEvents.Remove(enEvent);
        }
    }
}
