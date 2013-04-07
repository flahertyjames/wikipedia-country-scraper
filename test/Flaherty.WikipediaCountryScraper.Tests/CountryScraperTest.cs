using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flaherty.WikipediaCountryScraper.Tests
{
    [TestClass]
    public class CountryScraperTest
    {
        private static IEnumerable<Country> _countries;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var scraper = new CountryScraper();
            _countries = scraper.Scrape();
        }

        [TestMethod]
        public void CountryScraper_Scrape_ReturnsData()
        {
            Assert.IsTrue(_countries.Any());
        }

        [TestMethod]
        public void CountryScraper_Scrape_ContiansValidData()
        {
            var us = _countries.FirstOrDefault(x => x.IsoCode == "US");
            Assert.IsNotNull(us);
            Assert.AreEqual("United States", us.Name);
        }

        [TestMethod]
        public void CountryScraper_Scrape_ContainsGreatBritainAndNotEngland()
        {
            var gb = _countries.FirstOrDefault(x => x.IsoCode == "GB");
            var eng = _countries.FirstOrDefault(x => x.IsoCode == "ENG");
            Assert.IsNotNull(gb);
            Assert.IsNull(eng);
        }

        [TestMethod]
        public void CountryScraper_ScrapeSplitGreatBritain_ContainsEnglandAndNotGreatBritain()
        {
            var scraper = new CountryScraper(true);
            var countries = scraper.Scrape().ToList();
            var gb = countries.FirstOrDefault(x => x.IsoCode == "GB");
            var eng = countries.FirstOrDefault(x => x.IsoCode == "ENG");
            Assert.IsNull(gb);
            Assert.IsNotNull(eng);
        }
    }
}
