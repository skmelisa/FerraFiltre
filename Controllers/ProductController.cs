using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ferraFiltre.IServices;
using ferraFiltre.Services;
using ferraFiltre.Models;

namespace ferraFiltre.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly CrossReferenceService _referenceService;

        public ProductController(IProductService productService, CrossReferenceService referenceService)
        {
            _productService = productService;
            _referenceService = referenceService;
        }

        public async Task<IActionResult> Index(string filtreNo)
        {
            var productDetail = _productService.GetProductByFilterNo(filtreNo);  // Bu �r�n�n do�ru �ekilde al�nmas� gerekiyor

            if (productDetail == null)
            {
                return NotFound();
            }

            // CrossReferenceResult'� al�rken do�ru �ekilde kullanmal�y�z
            var crossReferences = _referenceService.GetReferences(productDetail.Product.filtre_no_b);

            var viewModel = new ProductDetailView
            {
                Product = productDetail.Product,
                CrossReferences = crossReferences
            };

            return View(viewModel);
        }




    }
}
