using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Flaherty.WikipediaCountryScraper
{
    public class CountryScraper : Scraper<Country>
    {
        private readonly bool _splitGreatBritain;

        public CountryScraper() : 
            base(new Uri("http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2"))
        {
        }

        public CountryScraper(bool splitGreatBritain) : this()
        {
            _splitGreatBritain = splitGreatBritain;
        }

        public override IEnumerable<Country> Scrape()
        {
            var countries = new List<Country>();
            var countryRows = Document.DocumentNode.SelectNodes("//table[@class='wikitable sortable']")[0].SelectNodes("tr");
            foreach (HtmlNode node in countryRows)
            {
                var tds = node.SelectNodes("td");
                if (tds != null)
                {
                    HtmlNode span;
                    var country = new Country();
                    country.IsoCode = tds[0].InnerText;
                    span = tds[1].SelectSingleNode("descendant::span[@class='sorttext']");
                    country.Name = span != null ? span.InnerText : tds[1].InnerText;
                    if (country.IsoCode == "GB")
                    {
                        if (_splitGreatBritain)
                        {
                            var gbData = new List<Country>();
                            gbData.Add(new Country {IsoCode = "ENG", Name = "England"});
                            gbData.Add(new Country {IsoCode = "NIR", Name = "Northern Ireland"});
                            gbData.Add(new Country {IsoCode = "SCT", Name = "Scotland"});
                            gbData.Add(new Country {IsoCode = "WLS", Name = "Wales"});
                            countries.AddRange(gbData);
                        }
                        else
                            countries.Add(country);
                    }
                    else
                        countries.Add(country);
                }
            }
            return countries;
        }

    }
}
