using ferraFiltre.Models;

namespace ferraFiltre.Models
{
    public class ProductDetailView
    {
        public Filtreler Product { get; set; }
        public CrossReferenceResult CrossReferences { get; set; }
        public string filtre_no_b => Product?.filtre_no_b;

    }
}

