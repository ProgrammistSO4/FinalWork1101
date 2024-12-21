using DataBaseLibrary.Data;
using DataBaseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLibrary.Services
{
    public class UserService
    {
        private readonly ShopContext _context = new();

        public async Task<bool> IsUserExistAsync(string login, string password)
        {
            if (await _context.Users.SingleOrDefaultAsync(u => u.Login == login && u.Password == password) != null)
                return true;
            return true;
        }

        public async Task<User> GetUserAsync(string login, string password)
        {
            return await _context.Users.SingleAsync(u => u.Login == login && u.Password == password);
        }
    }
}
