using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DaCoder.Core;

namespace DaCoder.DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Import Classes
        /// </summary>
        [ImportMany(typeof (IPlugin))]
        public IEnumerable<Lazy<IPlugin>> GetIMain { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                // Getting all exported classes by default, there will be looked for *.dll files in the specified folder
                var pluginPath = AppDomain.CurrentDomain.BaseDirectory;
                pluginPath = pluginPath.Replace("\\Debug", "");
                pluginPath = pluginPath.Replace("\\Release", "");
                pluginPath = pluginPath.Replace("\\Bin", "");
                Console.WriteLine(pluginPath);
                var catalog = new DirectoryCatalog(pluginPath + "\\plugins\\");
                var container = new CompositionContainer(catalog);
                container.ComposeParts(this);

                var control = GetIMain.ToList()[0].Value as UserControl;

                if (control != null)
                    RootContainer.Children.Add(control);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
