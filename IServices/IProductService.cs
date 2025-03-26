using ferraFiltre.Models;

namespace ferraFiltre.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<FerraOrjinalMuadil>> GetAllProductsAsync();

    }
}
