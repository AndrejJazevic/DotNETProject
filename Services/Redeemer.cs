using Domain.Entities;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Redeemer
    {
        private readonly ItemDbContext _context;
        
        public Redeemer(ItemDbContext context)
        {
            _context = context;
        }

        public async Task ChooseWinner(Guid itemId)
        {
            var item = await _context.Items.FindAsync(itemId);
            var applicants = item.Applicants;
            
            var rand = new Random();
            var winnerIndex = rand.Next(0, applicants.Count);

            var winner = applicants.Skip(winnerIndex).First();
            item.Redeemer = winner.User.Username;
            
            await _context.SaveChangesAsync();
        }

        public async Task SuspendItem(Guid ItemId)
        {
            var item = await _context.Items.FindAsync(ItemId);
            item.IsSuspended = true;
            await _context.SaveChangesAsync();
        }
        
    }
}
