using ferraFiltre.Models;
using System.Collections.Generic;
using System.Linq;

namespace ferraFiltre.Services
{
    public class CrossReferenceService
    {
        private readonly List<FerraOrjinalMuadil> _muadiller;

        public CrossReferenceService(List<FerraOrjinalMuadil> muadiller)
        {
            _muadiller = muadiller;
        }

        public CrossReferenceResult GetReferences(string ferraNo)
        {
            var related = _muadiller.Where(x => x.ferra_no_b == ferraNo).ToList();

            return new CrossReferenceResult
            {
                OEM = related.Where(x => x.orjinal_muadil == "1")
                      .Select(x => new CrossReferenceItem
                      {
                          FirmaAdi = x.firma_adi ?? string.Empty,
                          FiltreNoB = x.filtre_no_b ?? string.Empty
                      }).ToList(),

                Manufacturer = related.Where(x => x.orjinal_muadil == "2")
                               .Select(x => new CrossReferenceItem
                               {
                                   FirmaAdi = x.firma_adi ?? string.Empty,
                                   FiltreNoB = x.filtre_no_b ?? string.Empty
                               }).ToList()
            };
        }
    }
}