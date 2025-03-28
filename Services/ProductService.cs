using ferraFiltre.Models;
using ferraFiltre.Data;
using ferraFiltre.IServices;
using ferraFiltre.Services;
using Microsoft.EntityFrameworkCore;


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
            return await _context.FerraOrjinalMuadil.ToListAsync();
        }

        public ProductDetailView? GetProductByFilterNo(string filtreNo)
        {
            var filtre = _context.Filtreler
                .FirstOrDefault(f => f.filtre_no_b == filtreNo);

            if (filtre == null)
            {
                return null;  
            }

            var crossReferenceResult = _context.CrossReferenceResults
                .FirstOrDefault(c => c.FiltreNoB == filtreNo);

            return new ProductDetailView
            {
                Product = filtre,  
                CrossReferences = crossReferenceResult  
            };
        }
    }
}