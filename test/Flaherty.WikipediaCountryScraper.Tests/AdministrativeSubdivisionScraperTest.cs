using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flaherty.WikipediaCountryScraper.Tests
{
    [TestClass]
    public class AdministrativeSubdivisionScraperTest
    {
        private static IEnumerable<AdministrativeSubdivision> _usSubdivisions;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var scraper = new AdministrativeSubdivisionScraper("US");
            _usSubdivisions = scraper.Scrape();
        }

        [TestMethod]
        public void AdministrativeSubdivisionScraper_Scrape_ReturnsData()
        {
            Assert.IsTrue(_usSubdivisions.Any());
        }

        [TestMethod]
        public void AdministrativeSubdivisionScraper_Scrape_ContiansValidData()
        {
            var ny = _usSubdivisions.FirstOrDefault(x => x.IsoCode == "US-NY");
            Assert.IsNotNull(ny);
            Assert.AreEqual("New York", ny.Name);
        }
    }
}