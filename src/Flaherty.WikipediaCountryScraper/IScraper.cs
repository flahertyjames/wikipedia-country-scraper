// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IScraper.cs" company="James Flaherty">
//   2013
// </copyright>
// <summary>
//   Defines the IScraper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.WikipediaCountryScraper
{
    using System.Collections.Generic;

    /// <summary>
    /// The Scraper interface.
    /// </summary>
    /// <typeparam name="T">
    /// Object type to be returned by scraper
    /// </typeparam>
    public interface IScraper<out T>
    {
        /// <summary>
        /// Runs the scrape.
        /// </summary>
        /// <returns>
        /// An <see>
        ///        <cref>IEnumerable</cref>
        ///    </see> of type T
        /// </returns>
        IEnumerable<T> Scrape();
    }
}
