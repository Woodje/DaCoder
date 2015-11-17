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

namespace DaCoder.DesktopClient.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly MainWindow mainWindow;

        public string RichTextBoxText { get; set; }

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// Gets the command that makes it possible to add a new plugin.
        /// </summary>
        public ActionCommand AddPluginDialogCommand
        {
            get { return new ActionCommand(parameter => AddPluginDialog()); }
        }

        private void AddPluginDialog()
        {
            var pluginInstallerDialog = new PluginInstallerDialog(mainWindow);
            pluginInstallerDialog.DataContext = new PluginInstallerViewModel(pluginInstallerDialog);
            pluginInstallerDialog.ShowDialog();
        }

    }
}