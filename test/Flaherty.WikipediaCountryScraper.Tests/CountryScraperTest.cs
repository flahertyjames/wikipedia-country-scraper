// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountryScraperTest.cs" company="James Flaherty">
//   2013
// </copyright>
// <summary>
//   Defines the CountryScraperTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.WikipediaCountryScraper
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    /// <summary>
    /// The country scraper test.
    /// </summary>
    [TestFixture]
    public class CountryScraperTest
    {
        /// <summary>
        /// The countries.
        /// </summary>
        private IEnumerable<Country> countries;

        /// <summary>
        /// Initializes the test fixture.
        /// </summary>
        [SetUp]
        public void Init()
        {
            var scraper = new CountryScraper();
            this.countries = scraper.Scrape();
        }

        /// <summary>
        /// The country scraper scrape returns data.
        /// </summary>
        [Test]
        public void CountryScraperScrapeReturnsData()
        {
            Assert.IsTrue(this.countries.Any());
        }

        /// <summary>
        /// The country scraper scrape contains valid data.
        /// </summary>
        [Test]
        public void CountryScraperScrapeContainsValidData()
        {
            var us = this.countries.FirstOrDefault(x => x.IsoCode == "US");
            Assert.IsNotNull(us);
            Assert.AreEqual("United States", us.Name);
        }

        /// <summary>
        /// The country scraper scrape contains great britain and not england.
        /// </summary>
        [Test]
        public void CountryScraperScrapeContainsGreatBritainAndNotEngland()
        {
            var gb = this.countries.FirstOrDefault(x => x.IsoCode == "GB");
            var eng = this.countries.FirstOrDefault(x => x.IsoCode == "ENG");
            Assert.IsNotNull(gb);
            Assert.IsNull(eng);
        }

        /// <summary>
        /// The country scraper scrape split great britain contains england and not great britain.
        /// </summary>
        [Test]
        public void CountryScraperScrapeSplitGreatBritainContainsEnglandAndNotGreatBritain()
        {
            var scraper = new CountryScraper(true);
            var scrapedCountries = scraper.Scrape().ToList();
            var gb = scrapedCountries.FirstOrDefault(x => x.IsoCode == "GB");
            var eng = scrapedCountries.FirstOrDefault(x => x.IsoCode == "ENG");
            Assert.IsNull(gb);
            Assert.IsNotNull(eng);
        }
    }
}
