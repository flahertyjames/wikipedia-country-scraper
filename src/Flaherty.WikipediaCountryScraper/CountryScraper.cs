// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountryScraper.cs" company="James Flaherty">
//   2013
// </copyright>
// <summary>
//   Defines the CountryScraper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.WikipediaCountryScraper
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class to scrape country data from Wikipedia.
    /// </summary>
    public class CountryScraper : Scraper<Country>
    {
        /// <summary>
        /// Specifies whether Great Britain should be split into its 4 countries.
        /// </summary>
        private readonly bool splitGreatBritain;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryScraper"/> class.
        /// </summary>
        public CountryScraper() : 
            base(new Uri("http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryScraper"/> class.
        /// </summary>
        /// <param name="splitGreatBritain">
        /// Specifies whether Great Britain should be split into its 4 countries.
        /// </param>
        public CountryScraper(bool splitGreatBritain) : this()
        {
            this.splitGreatBritain = splitGreatBritain;
        }

        /// <summary>
        /// Runs the scrape.
        /// </summary>
        /// <returns>
        /// An <see>
        ///        <cref>IEnumerable</cref>
        ///    </see> of type <see cref="Country"/>
        /// </returns>
        public override IEnumerable<Country> Scrape()
        {
            var countries = new List<Country>();
            var countryRows = Document.DocumentNode.SelectNodes("//table[@class='wikitable sortable']")[0].SelectNodes("tr");
            foreach (var node in countryRows)
            {
                var tds = node.SelectNodes("td");
                if (tds != null)
                {
                    var country = new Country { IsoCode = tds[0].InnerText };
                    var span = tds[1].SelectSingleNode("descendant::span[@class='sorttext']");
                    country.Name = span != null ? span.InnerText : tds[1].InnerText;
                    if (country.IsoCode == "GB")
                    {
                        if (this.splitGreatBritain)
                        {
                            var data = new List<Country>
                                           {
                                               new Country { IsoCode = "ENG", Name = "England" },
                                               new Country { IsoCode = "NIR", Name = "Northern Ireland" },
                                               new Country { IsoCode = "SCT", Name = "Scotland" },
                                               new Country { IsoCode = "WLS", Name = "Wales" }
                                           };
                            countries.AddRange(data);
                        }
                        else
                        {
                            countries.Add(country);
                        }
                    }
                    else
                    {
                        countries.Add(country);
                    }
                }
            }

            return countries;
        }
    }
}
