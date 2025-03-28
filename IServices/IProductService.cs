using ferraFiltre.Models;
using ferraFiltre.IServices;


namespace ferraFiltre.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<FerraOrjinalMuadil>> GetAllProductsAsync();

        ProductDetailView? GetProductByFilterNo(string filterNo);


    }
}
