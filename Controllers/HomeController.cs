using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ferraFiltre.IServices;
using ferraFiltre.Services;
using ferraFiltre.Models;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly FilterSearchService _filterSearchService; 

    private readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "ferra_orjinal_muadil.xlsx");

  
    public HomeController(IProductService productService, FilterSearchService filterSearchService)
    {
        _productService = productService;
        _filterSearchService = filterSearchService;
    }

    public async Task<IActionResult> Index(string searchTerm)
    {
        // 1. Verileri al
        List<FiltreData> excelData = ExcelService.ReadExcel(filePath);
        var products = await _productService.GetAllProductsAsync();

        // 2. Verileri birleþtir
        var combinedData = excelData.Concat(products.Select(p => new FiltreData
        {
            FerraNo = p.ferra_no_b,
            FirmaAdi = p.firma_adi,
            FiltreNo = p.filtre_no_b,
            FiltreNoGoster = p.filtre_no_goster,
            Katalog = p.hangi_katalog,
            OrjinalMuadil = p.orjinal_muadil
        })).ToList();

        // 3. Arama yapýldýysa filtrele
        if (!string.IsNullOrEmpty(searchTerm))
        {
            string cleanedQuery = Regex.Replace(searchTerm, @"[^a-zA-Z0-9]", "").ToLower();
            combinedData = combinedData
                .Where(x => (x.FiltreNoGoster ?? "").Replace(" ", "").ToLower()
                    .Contains(cleanedQuery))
                .ToList();
        }

        // 4. SearchResult'a dönüþtür (View'ýn beklediði modele uygun hale getir)
        var viewModel = combinedData.Select(x => new SearchResult
        {
            ferra_no_b = x.FerraNo,
            firma_adi = x.FirmaAdi,
            filtre_no_b = x.FiltreNo,
            filtre_durumu = x.Katalog,
            foto1 = x.OrjinalMuadil
        }).ToList();

        return View(viewModel); // Doðru model tipini gönder
    }


    public IActionResult ProductDetails(string filterNo)
    {
        var productDetail = _productService.GetProductByFilterNo(filterNo);  // await kullanma

        if (productDetail == null)
        {
            return NotFound();  // Eðer ürün bulunamazsa 404 döndür
        }

        return View(productDetail);  // ProductDetailView'i View'a gönder
    }


}
