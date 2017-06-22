using EmailManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Data.Repository
{
    public interface IEnEventRepository : IRepo<EnEvent>
    {
        Task<EnEvent> GetEventsByNameAsync(string name);

        Task<EnEvent> GetEventsById(int id);

        Task<IEnumerable<EnEvent>> GetAllEvents();

        void AddEvent(EnEvent enEvent);

        void DeleteEvent(EnEvent enEvent);
    }
}
