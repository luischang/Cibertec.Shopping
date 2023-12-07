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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDbcibertecContext _context;

        public CategoryRepository(StoreDbcibertecContext context)
        {
            _context = context;
        }

        public async Task<bool> Insert(Category category)
        {
            await _context.Category.AddAsync(category);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Update(Category category)
        {
            _context.Category.Update(category);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
                return false;

            category.IsActive = false;
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Category.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Category.Where(x => x.Id == id && x.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task<Category> GetByIdWithProducts(int id)
        {
            return await _context
                .Category
                .Where(x => x.Id == id && x.IsActive == true)
                .Include(z => z.Product)
                .FirstOrDefaultAsync();
        }


    }
}
