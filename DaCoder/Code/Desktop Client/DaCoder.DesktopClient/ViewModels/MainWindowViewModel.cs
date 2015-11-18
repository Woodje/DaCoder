using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using DaCoder.Core;
using DaCoder.DesktopClient.Views;
using System.Collections.Generic;
using DaCoder.Data;
using System.Linq;
using System.Collections.ObjectModel;

namespace DaCoder.DesktopClient.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly MainWindow mainWindow;

        public ICollection<Language> Languages { get; set; }

        public string RichTextBoxText { get; set; }

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            Languages = new ObservableCollection<Language>();

            using (var businessContext = new BusinessContext())
            {
                var queryAllLanguages =
                    from languages in businessContext.DataContext.Languages
                    select languages;

                foreach (var language in queryAllLanguages)
                {
                    Languages.Add(language);
                }
            }
        }

        /// <summary>
        /// Gets the command that makes it possible to add a new plugin.
        /// </summary>
        public ActionCommand AddPluginDialogCommand
        {
            get { return new ActionCommand(parameter => AddPluginDialog()); }
        }

        /// <summary>
        /// Opens up the plugin dialog.
        /// </summary>
        private void AddPluginDialog()
        {
            var pluginInstallerDialog = new PluginInstallerDialog(mainWindow);
            pluginInstallerDialog.DataContext = new PluginInstallerViewModel(pluginInstallerDialog);
            pluginInstallerDialog.ShowDialog();
        }

        /// <summary>
        /// Gets the command that makes it possible to edit the languages.
        /// </summary>
        public ActionCommand LanguageOptionDialogCommand
        {
            get { return new ActionCommand(parameter => LanguageOptionDialog()); }
        }

        /// <summary>
        /// Opens up the language options dialog.
        /// </summary>
        private void LanguageOptionDialog()
        {
            var languageOptionDialog = new LanguageOptionDialog();
            languageOptionDialog.DataContext = new LanguageOptionViewModel();
            languageOptionDialog.ShowDialog();
        }

    }
}