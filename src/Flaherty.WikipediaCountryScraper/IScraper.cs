using System.Collections.Generic;

namespace Flaherty.WikipediaCountryScraper
{
    public interface IScraper<T>
    {
        IEnumerable<T> Scrape();
    }
}
