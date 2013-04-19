// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scraper.cs" company="James Flaherty">
//   2013
// </copyright>
// <summary>
//   Defines the Scraper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.WikipediaCountryScraper
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using HtmlAgilityPack;

    /// <summary>
    /// Abstract scraper class.
    /// </summary>
    /// <typeparam name="T">
    /// Object type to be returned by scraper
    /// </typeparam>
    public abstract class Scraper<T> : IScraper<T>
    {
        /// <summary>
        /// HtmlDocument containing response of the scraped URL
        /// </summary>
        private readonly HtmlDocument document = new HtmlDocument();

        /// <summary>
        /// Initializes a new instance of the <see cref="Scraper{T}"/> class.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        protected Scraper(Uri uri)
        {
            var request = WebRequest.Create(uri) as HttpWebRequest;
            if (request != null)
            {
                request.Method = "GET";
                request.UserAgent =
                    "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.0.8) Gecko/2009032609 Firefox/3.0.8 (.NET CLR 3.5.30729)";
                var response = request.GetResponse() as HttpWebResponse;
                if (response != null)
                {
                    this.document.Load(response.GetResponseStream(), true);
                }
            }
        }

        /// <summary>
        /// Gets the HtmlDocument containing response of the scraped URL
        /// </summary>
        protected HtmlDocument Document
        {
            get { return this.document; }
        }

        /// <summary>
        /// Runs the scrape.
        /// </summary>
        /// <returns>
        /// An <see>
        ///        <cref>IEnumerable</cref>
        ///    </see> of type T
        /// </returns>
        public abstract IEnumerable<T> Scrape();
    }
}
