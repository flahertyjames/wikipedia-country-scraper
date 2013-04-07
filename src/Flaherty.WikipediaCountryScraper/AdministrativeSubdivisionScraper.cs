using System;
using System.Collections.Generic;
using System.Linq;

namespace Flaherty.WikipediaCountryScraper
{
    public class AdministrativeSubdivisionScraper : Scraper<AdministrativeSubdivision>
    {
        private readonly string _countryCode;

        public AdministrativeSubdivisionScraper(string countryCode) : 
            base(new Uri("http://en.wikipedia.org/wiki/ISO_3166-2:" + GetCountryCode(countryCode)))
        {
            _countryCode = countryCode;
        }

        public override IEnumerable<AdministrativeSubdivision> Scrape()
        {
            var subdivisions = new List<AdministrativeSubdivision>();
            var tables = Document.DocumentNode.SelectNodes("//table");
            if (tables != null)
            {
                var exit = false;
                foreach (var table in tables)
                {
                    var subRows = table.SelectNodes("tr");
                    foreach (var subNode in subRows)
                    {
                        var subThs = subNode.SelectNodes("th");
                        var subTds = subNode.SelectNodes("td");
                        if (subThs != null && subThs[0].InnerText.Contains("Newsletter"))
                        {
                            exit = true;
                            break;
                        }
                        if (subTds != null && subTds[0].InnerText.StartsWith(_countryCode + "-") && !subTds[1].InnerText.StartsWith(_countryCode + "-"))
                        {
                            var subdivision = new AdministrativeSubdivision { CountryIsoCode = _countryCode };
                            var span = subTds[0].SelectSingleNode("descendant::span[@class='sorttext']");
                            subdivision.IsoCode = span != null ? span.InnerText : subTds[0].InnerText;
                            var a = subTds[1].SelectSingleNode("descendant::a");
                            if (a != null)
                                subdivision.Name = a.InnerText;
                            else
                            {
                                span = subTds[1].SelectSingleNode("descendant::span[@class='sorttext']");
                                subdivision.Name = span != null ? span.InnerText : subTds[1].InnerText;
                            }
                            if (IsCountryInGreatBritain(_countryCode) && subTds[3].InnerText != _countryCode)
                                subdivision = null;
                            if (subdivision != null &&
                                subdivisions.FirstOrDefault(x => x.IsoCode == subdivision.IsoCode) == null)
                            {
                                subdivisions.Add(subdivision);
                            }
                        }
                    }
                    if (exit)
                        break;
                }
            }
            return subdivisions;
        }

        protected static string GetCountryCode(string countryCode)
        {
            return IsCountryInGreatBritain(countryCode) ? "GB" : countryCode;
        }

        protected static bool IsCountryInGreatBritain(string countryCode)
        {
            return new[] {"ENG", "NIR", "SCT", "WLS"}.Contains(countryCode);
        }

    }
}
