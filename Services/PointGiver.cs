using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PointGiver
    {
        private readonly ItemDbContext _context;

        public PointGiver(ItemDbContext context)
        {
            _context = context;
        }

        public async Task GivePoints(string user, int amount)
        {
            var User = await _context.Users.FindAsync(user);
            User.Points += amount;
            await _context.SaveChangesAsync();
        }

        public async Task GivePointsOnRedemption(string user , Guid ItemId)
        {
            await GivePoints(user, CalculatePointsForRedemption(ItemId));
        }

        private int CalculatePointsForRedemption(Guid ItemId)
        {
            var QualityRating = Int32.Parse(_context.Ratings.Where(s => s.ItemId == ItemId).Average(s => s.QualityRating).ToString());
            var PriceRating = Int32.Parse(_context.Ratings.Where(s => s.ItemId == ItemId).Average(s => s.PriceRating).ToString());
            int Points = PriceRating * 100 * (QualityRating / 10);
            return Points;
        }
    }
}
