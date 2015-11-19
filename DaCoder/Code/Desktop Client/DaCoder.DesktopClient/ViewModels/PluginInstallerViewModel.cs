using System.ComponentModel.DataAnnotations;
using System.IO;
using DaCoder.Core;
using DaCoder.DesktopClient.Views;
using Microsoft.Win32;

namespace DaCoder.DesktopClient.ViewModels
{
    /// <summary>
    /// A View-Model that represents a plugininstallerdialog.
    /// </summary>
    public class PluginInstallerViewModel : ViewModel
    {
        private string filePath;
        private string fileName;
        private readonly PluginInstallerDialog pluginInstallerDialog;

        public PluginInstallerViewModel(PluginInstallerDialog pluginInstallerDialog)
        {
            this.pluginInstallerDialog = pluginInstallerDialog;
        }

        /// <summary>
        /// Gets or sets the filepaths value.
        /// </summary>
        [Required]
        public string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the command that installs the chosen plugin.
        /// </summary>
        public ActionCommand InstallPluginCommand
        {
            get
            {
                return new ActionCommand(parameter => InstallPlugin(),
                                         parameter => FilePath != null && FilePath.EndsWith(".dll"));
            }
        }

        /// <summary>
        /// Installs the chosen plugin.
        /// </summary>
        private void InstallPlugin()
        {
            pluginInstallerDialog.Close();

            if (File.Exists(pluginInstallerDialog.MainWindow.PluginPath + fileName))
            {
                File.Delete(pluginInstallerDialog.MainWindow.PluginPath + fileName);
            }

            File.Copy(FilePath, pluginInstallerDialog.MainWindow.PluginPath + fileName);

            pluginInstallerDialog.MainWindow.InitializePlugins();
        }

        /// <summary>
        /// Gets the command that makes it possible to browse for a plugin file.
        /// </summary>
        public ActionCommand BrowseForPluginFileCommand
        {
            get
            {
                return new ActionCommand(parameter => BrowseForPluginFile());
            }
        }

        /// <summary>
        /// Makes it possible to browse for a plugin file.
        /// </summary>
        public void BrowseForPluginFile()
        {
            var openFileDialog = new OpenFileDialog
                                 {
                                     Filter = "DaCoder plugin|*.dll"
                                 };

            openFileDialog.ShowDialog();

            fileName = openFileDialog.SafeFileName;

            FilePath = openFileDialog.FileName;
        }
    }
}