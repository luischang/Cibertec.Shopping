using Cibertec.Shopping.CORE.Entities;
using Cibertec.Shopping.CORE.Interfaces;
using Cibertec.Shopping.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Shopping.INFRASTRUCTURE.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly StoreDbcibertecContext _context;

        public FavoriteRepository(StoreDbcibertecContext context)
        {
            _context = context;
        }

        public async Task<bool> Insert(Favorite favorite)
        {
            await _context.Favorite.AddAsync(favorite);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var favorite = await _context.Favorite.FindAsync(id);
            if (favorite == null)
                return false;

            _context.Remove(favorite);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<IEnumerable<Favorite>> GetAll(int userId)
        {
            return await _context
                        .Favorite
                        .Where(f => f.UserId == userId)
                        .Include(u => u.User)
                        .Include(p => p.Product)
                        .ThenInclude(c => c.Category)
                        .ToListAsync();
        }
    }
}
