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
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbcibertecContext _context;

        public ProductRepository(StoreDbcibertecContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Product
                .Where(x => x.IsActive == true)
                .Include(z => z.Category)
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Product
                    .Where(x => x.Id == id)
                    .Include(z => z.Category)
                    .FirstOrDefaultAsync();
        }

        public async Task<bool> Insert(Product product)
        {
            await _context.Product.AddAsync(product);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Update(Product product)
        {
            _context.Product.Update(product);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var findProduct = await _context
                            .Product
                            .Where(x => x.Id == id && x.IsActive == true)
                            .FirstOrDefaultAsync();
            if (findProduct == null)
                return false;

            findProduct.IsActive = false;
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

    }
}
