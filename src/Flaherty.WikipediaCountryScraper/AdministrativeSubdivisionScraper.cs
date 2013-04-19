// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdministrativeSubdivisionScraper.cs" company="James Flaherty">
//   2013
// </copyright>
// <summary>
//   Defines the AdministrativeSubdivisionScraper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.WikipediaCountryScraper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The administrative subdivision scraper.
    /// </summary>
    public class AdministrativeSubdivisionScraper : Scraper<AdministrativeSubdivision>
    {
        /// <summary>
        /// The country code.
        /// </summary>
        private readonly string countryCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministrativeSubdivisionScraper"/> class.
        /// </summary>
        /// <param name="countryCode">
        /// The country code.
        /// </param>
        public AdministrativeSubdivisionScraper(string countryCode) : 
            base(new Uri("http://en.wikipedia.org/wiki/ISO_3166-2:" + GetCountryCode(countryCode)))
        {
            this.countryCode = countryCode;
        }

        /// <summary>
        /// Runs the scrape.
        /// </summary>
        /// <returns>
        /// An <see>
        ///        <cref>IEnumerable</cref>
        ///    </see> of type <see cref="AdministrativeSubdivision"/>
        /// </returns>
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

                        if (subTds != null && subTds[0].InnerText.StartsWith(this.countryCode + "-") && !subTds[1].InnerText.StartsWith(this.countryCode + "-"))
                        {
                            var subdivision = new AdministrativeSubdivision { CountryIsoCode = this.countryCode };
                            var span = subTds[0].SelectSingleNode("descendant::span[@class='sorttext']");
                            subdivision.IsoCode = span != null ? span.InnerText : subTds[0].InnerText;
                            var a = subTds[1].SelectSingleNode("descendant::a");
                            if (a != null)
                            {
                                subdivision.Name = a.InnerText;
                            }
                            else
                            {
                                span = subTds[1].SelectSingleNode("descendant::span[@class='sorttext']");
                                subdivision.Name = span != null ? span.InnerText : subTds[1].InnerText;
                            }

                            if (IsCountryInGreatBritain(this.countryCode) && subTds[3].InnerText != this.countryCode)
                            {
                                subdivision = null;
                            }

                            if (subdivision != null
                                && subdivisions.FirstOrDefault(x => x.IsoCode == subdivision.IsoCode) == null)
                            {
                                subdivisions.Add(subdivision);
                            }
                        }
                    }

                    if (exit)
                    {
                        break;
                    }
                }
            }

            return subdivisions;
        }

        /// <summary>
        /// The get country code.
        /// </summary>
        /// <param name="countryCode">
        /// The country code.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected static string GetCountryCode(string countryCode)
        {
            return IsCountryInGreatBritain(countryCode) ? "GB" : countryCode;
        }

        /// <summary>
        /// The is country in great britain.
        /// </summary>
        /// <param name="countryCode">
        /// The country code.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected static bool IsCountryInGreatBritain(string countryCode)
        {
            return new[] { "ENG", "NIR", "SCT", "WLS" }.Contains(countryCode);
        }
    }
}
