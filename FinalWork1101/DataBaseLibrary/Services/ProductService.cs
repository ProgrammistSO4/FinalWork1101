using DataBaseLibrary.Data;
using DataBaseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLibrary.Services
{
    public class ProductService
    {
        private readonly ShopContext _context = new();

        public async Task<List<Product>> GetProductsAsync(string subSTR, int? sort, decimal minCost, decimal? maxCost, string? manuf)
        {
            IQueryable<Product> products = _context.Products.AsNoTracking();

            if (!string.IsNullOrEmpty(subSTR))
            {
                products = products.Where(p => EF.Functions.Like(p.Name, $"%{subSTR}%"));
            }

            products = products.Where(p => p.Cost >= minCost);

            if (maxCost.HasValue)
            {
                products = products.Where(p => p.Cost <= maxCost);
            }
            if (manuf == "System.Windows.Controls.ComboBoxItem: Все производители")
            {
                manuf = "";
            }
            if (!string.IsNullOrEmpty(manuf))
            {
                products = products.Where(p => p.Manufacturer == manuf);
            }
            // Сортировка
            if (sort == 0)
            {
                products = products.OrderBy(p => p.Cost);
            }
            else if (sort == 1)
            {
                products = products.OrderByDescending(p => p.Cost);
            }
            string query = products.ToQueryString();

            return await products.ToListAsync();
        }

        public async Task<List<string>> GetManufacturersAsync()
        {
            var manufacturers = await _context.Products.Select(p => p.Manufacturer).Distinct()
                .Where(m => !string.IsNullOrEmpty(m)).OrderBy(m => m).ToListAsync();
            return manufacturers;
        }

        public async Task<int> GetProductsCountAsync()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return products.Count();
        }
    }
}
