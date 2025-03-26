using Microsoft.EntityFrameworkCore;
using ferraFiltre.Data;
using ferraFiltre.IServices;
using ferraFiltre.Models;


namespace ferraFiltre.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FerraOrjinalMuadil>> GetAllProductsAsync()
        {
            return await _context.FiltrelerIlk.ToListAsync();
        }
    }
}
