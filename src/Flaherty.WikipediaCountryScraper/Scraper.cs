using System;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;

namespace Flaherty.WikipediaCountryScraper
{
    public abstract class Scraper<T> : IScraper<T>
    {
        private readonly HtmlDocument _document = new HtmlDocument();

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
                    _document.Load(response.GetResponseStream(), true);
            }
        }

        protected HtmlDocument Document
        {
            get { return _document; }
        }

        public abstract IEnumerable<T> Scrape();
    }
}
