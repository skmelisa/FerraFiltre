using System.ComponentModel.DataAnnotations;
namespace ferraFiltre.Models
{
    public class FerraOrjinalMuadil
    {
        [Key]
        public string ferra_no_b { get; set; }
        public string firma_adi { get; set; }
        public string filtre_no_b { get; set; }
        public string filtre_no_goster { get; set; }
        public string hangi_katalog { get; set; }
        public string orjinal_muadil { get; set; }
        public string sabit_degisken { get; set; }
    }
}
