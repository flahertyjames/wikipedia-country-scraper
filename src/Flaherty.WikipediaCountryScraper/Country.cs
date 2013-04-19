// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Country.cs" company="James Flaherty">
//   2013
// </copyright>
// <summary>
//   Defines the Country type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.WikipediaCountryScraper
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The country.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Gets or sets the iso code.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string IsoCode { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
