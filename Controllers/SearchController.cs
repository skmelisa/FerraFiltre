using Microsoft.AspNetCore.Mvc;
using ferraFiltre.Services;
using ferraFiltre.Models;

namespace ferraFiltre.Controllers
{
    public class SearchController : Controller
    {
        private readonly FilterSearchService _searchService;
        private readonly CrossReferenceService _referenceService;
        private readonly ProductService _productService;

        public SearchController(FilterSearchService searchService, CrossReferenceService referenceService, ProductService productService)
        {
            _searchService = searchService;
            _referenceService = referenceService;
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(string query)
        {
            var results = _searchService.Search(query);
            return View("Results", results);
        }

        [HttpGet]
        public IActionResult Detail(string ferraNo)
        {
            if (string.IsNullOrEmpty(ferraNo))
            {
                return BadRequest("Filtre No is required.");
            }

            var product = _productService.GetProductByFilterNo(ferraNo);

            if (product == null)
            {
                return NotFound();
            }

            var crossReferences = _referenceService.GetReferences(product.Product.ferra_no_bosluksuz);

            var viewModel = new ProductDetailView
            {
                Product = product.Product,
                CrossReferences = crossReferences
            };

            return View(viewModel);
        }

    }
}
