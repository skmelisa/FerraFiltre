using ferraFiltre.Models;
using ferraFiltre.Services;


namespace ferraFiltre.Services
{
        public class FilterSearchService
    {
                private readonly List<FerraOrjinalMuadil> _muadiller;
                private readonly List<Filtreler> _filtreler;

                public FilterSearchService(List<FerraOrjinalMuadil> muadiller, List<Filtreler> filtreler)
                {
                    _muadiller = muadiller ?? throw new ArgumentNullException(nameof(muadiller));
                    _filtreler = filtreler ?? throw new ArgumentNullException(nameof(filtreler));
                }

                public List<SearchResult> Search(string searchQuery)
                {
                    if (string.IsNullOrEmpty(searchQuery))
                        return new List<SearchResult>();

                    var sanitizedQuery = RemoveSpecialCharacters(searchQuery).ToLower();

                    var matches = _muadiller
                        .Where(x => !string.IsNullOrEmpty(x.filtre_no_goster) &&
                                    RemoveSpecialCharacters(x.filtre_no_goster)?.ToLower().Contains(sanitizedQuery) == true)
                        .ToList();

                    var priority = new[] { "RD", "ND", "SD", "YD", "DD" };
                    foreach (var p in priority)
                    {
                        var filtered = matches.Where(x => x.sabit_degisken == p).ToList();
                        if (filtered.Any())
                        {
                            matches = filtered;
                            break;
                        }
                    }

                    var results = matches.Select(m =>
                    {
                        var linked = _filtreler.FirstOrDefault(f => f.ferra_no_bosluksuz == m.ferra_no_b);
                        return new SearchResult
                        {
                            ferra_no_b = m.ferra_no_b,
                            firma_adi = m.firma_adi,
                            filtre_no_b = m.filtre_no_b,
                            filtre_durumu = linked?.filtre_durumu,
                            foto1 = linked?.foto1
                        };
                    }).ToList();

                    return results;
                }

                private string RemoveSpecialCharacters(string input)
                {
                    if (string.IsNullOrEmpty(input))
                        return string.Empty; 

                    return new string(input.Where(c => char.IsLetterOrDigit(c)).ToArray());
                }
            }
}


