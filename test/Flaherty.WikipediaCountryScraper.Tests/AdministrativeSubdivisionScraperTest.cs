// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdministrativeSubdivisionScraperTest.cs" company="James Flaherty">
//   2013
// </copyright>
// <summary>
//   The administrative subdivision scraper test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.WikipediaCountryScraper
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using NUnit.Framework;

    /// <summary>
    /// The administrative subdivision scraper test.
    /// </summary>
    [TestFixture]
    public class AdministrativeSubdivisionScraperTest
    {
        /// <summary>
        /// The us subdivisions.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        private IEnumerable<AdministrativeSubdivision> usSubdivisions;

        /// <summary>
        /// The class initialize.
        /// </summary>
        [SetUp]
        public void ClassInitialize()
        {
            var scraper = new AdministrativeSubdivisionScraper("US");
            this.usSubdivisions = scraper.Scrape();
        }

        /// <summary>
        /// The administrative subdivision scraper scrape returns data.
        /// </summary>
        [Test]
        public void AdministrativeSubdivisionScraperScrapeReturnsData()
        {
            Assert.IsTrue(this.usSubdivisions.Any());
        }

        /// <summary>
        /// The administrative subdivision scraper scrape contains valid data.
        /// </summary>
        [Test]
        public void AdministrativeSubdivisionScraperScrapeContainsValidData()
        {
            var ny = this.usSubdivisions.FirstOrDefault(x => x.IsoCode == "US-NY");
            Assert.IsNotNull(ny);
            Assert.AreEqual("New York", ny.Name);
        }
    }
}