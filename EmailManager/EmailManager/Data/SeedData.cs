using EmailManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Data
{
    public class SeedData
    {
        private DatabaseContext _context;
        private IUnitOfWork _unitOfWork;
        private UserManager<User> _userManager;

        public SeedData(DatabaseContext context, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task EnsureSeed()
        {
            if (await _userManager.FindByEmailAsync("diana.diaconu@endava.com") == null)
            {
                var user = new User()
                {
                    UserName = "dianadiaconu",
                    Name = "Diana Diaconu",
                    Email = "diana.diaconu@endava.com"
                };

                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }

            if (!_context.EnEvents.Any())
            {
                await Seed();
            }
        }

        private async Task Seed()
        {
            var enEventsRepo = _unitOfWork.GetRepository<EnEvent>();

            var summerLogout = new EnEvent()
            {
                Name = "Summer Logout",
                Location = "Ambasad'Or Events",
                EventDate = DateTime.Parse("Jul 14, 2017")
            };

            var kickOff = new EnEvent()
            {
                Name = "Kick Off 2017",
                Location = "Phoenicia",
                EventDate = DateTime.Parse("Oct 18, 2017")
            };

            var xmasParty = new EnEvent()
            {
                Name = "Christmas Party",
                Location = "Hard Rock Cafe",
                EventDate = DateTime.Parse("Dec 19, 2017")
            };

            await enEventsRepo.InsertAsync(summerLogout);
            await enEventsRepo.InsertAsync(kickOff);
            await enEventsRepo.InsertAsync(xmasParty);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
