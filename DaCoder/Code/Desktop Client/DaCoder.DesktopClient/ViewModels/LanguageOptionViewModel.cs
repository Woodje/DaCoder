using DaCoder.Core;
using DaCoder.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DaCoder.DesktopClient.ViewModels
{
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
