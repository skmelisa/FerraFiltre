using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ferraFiltre.IServices;

public class HomeController : Controller
{
    private readonly IProductService _productService;


    private readonly string filePath = @"C:\Users\Melisa2\Desktop\ferra_orjinal_muadil.xlsx";

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }
    //deneme


    public async Task<IActionResult> Index(string searchTerm)
    {
        List<FiltreData> data = ExcelService.ReadExcel(filePath);
        var products = await _productService.GetAllProductsAsync();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            string cleanedQuery = Regex.Replace(searchTerm, @"[^a-zA-Z0-9]", "");
            data = data.Where(x => Regex.Replace(x.FiltreNoGoster, @"[^a-zA-Z0-9]", "")
                                .Contains(cleanedQuery, StringComparison.OrdinalIgnoreCase))
                       .ToList();
        }

        return View(data);
    }

  

 

}
