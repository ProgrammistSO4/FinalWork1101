using DataBaseLibrary.Data;
using DataBaseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLibrary.Services
{
    public class OrderService
    {
        private readonly ShopContext _context = new();
    }
}
