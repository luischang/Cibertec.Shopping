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
    public class UserRepository : IUserRepository
    {
        private readonly StoreDbcibertecContext _context;
        public UserRepository(StoreDbcibertecContext context)
        {
            _context = context;
        }

        public async Task<bool> Insert(User user)
        {
            await _context.User.AddAsync(user);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
                return false;

            user.IsActive = false;
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<User> GetUserByCredentials(string email, string password)
        {
            return await _context
                        .User
                        .Where(u => u.Email == email
                                && u.Password == password
                                && u.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context
                        .User
                        .Where(u => u.Id == id)
                        .FirstOrDefaultAsync();
        }

        public async Task<bool> GetByEmail(string email)
        {
            return await _context.User.Where(u => u.Email == email).AnyAsync();
        }
    }
}
