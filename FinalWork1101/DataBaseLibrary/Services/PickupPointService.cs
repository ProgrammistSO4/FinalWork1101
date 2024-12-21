using DataBaseLibrary.Data;
using DataBaseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLibrary.Services
{
    public class PickupPointService
    {
        private readonly ShopContext _context = new();
    }
}
