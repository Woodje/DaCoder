using DaCoder.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DaCoder.DesktopClient.ViewModels
{
    /// <summary>
    /// A View-Model that represents a languageoptiondialog.
    /// </summary>
    public class LanguageOptionViewModel
    {
        /// <summary>
        /// Gets the collection of language entities.
        /// </summary>
        public ICollection<Language> Languages { get; set; }

        public LanguageOptionViewModel()
        {
            ReloadLanguageOptionsData();
        }

        /// <summary>
        /// Reloads the languages.
        /// </summary>
        public void ReloadLanguageOptionsData()
        {
            Languages = new ObservableCollection<Language>();

            using (var businessContext = new BusinessContext())
            {
                var queryAllLanguages =
                    from languages in businessContext.DataContext.Languages
                    select languages;

                foreach (var language in queryAllLanguages)
                {
                    Languages.Add(language);

                    // Accessing the languages keywords entities to make sure that it is available.
                    if (language.Keywords != null) ;
                }
            }
        }
    }
}
