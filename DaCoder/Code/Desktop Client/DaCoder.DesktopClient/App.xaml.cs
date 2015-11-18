using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DaCoder.DesktopClient.ViewModels;
using DaCoder.DesktopClient.Views;

namespace DaCoder.DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow();
            mainWindow.DataContext = new MainWindowViewModel(mainWindow);
            mainWindow.MainWindowViewModel = (MainWindowViewModel) mainWindow.DataContext;
            mainWindow.ShowDialog();
        }
    }
}
