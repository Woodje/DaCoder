using System;
using System.Windows;
using DaCoder.Core;
using DaCoder.DesktopClient.Views;

namespace DaCoder.DesktopClient.ViewModels
{
    public class PluginInstallerViewModel : ViewModel
    {
        /// <summary>
        /// Gets the command that makes it possible to add a new plugin.
        /// </summary>
        public ActionCommand AddPluginCommand
        {
            get { return new ActionCommand(p => (new PluginInstallerDialog()).ShowDialog()); }
        }

    }
}