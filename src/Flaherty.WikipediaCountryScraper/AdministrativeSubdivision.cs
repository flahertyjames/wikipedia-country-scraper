// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdministrativeSubdivision.cs" company="James Flaherty">
//   2013
// </copyright>
// <summary>
//   Defines the AdministrativeSubdivision type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Flaherty.WikipediaCountryScraper
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The administrative subdivision.
    /// </summary>
    public class AdministrativeSubdivision
    {
        /// <summary>
        /// Gets or sets the country iso code.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string CountryIsoCode { get; set; }

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
