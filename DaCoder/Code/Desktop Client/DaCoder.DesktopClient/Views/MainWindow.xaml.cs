using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using DaCoder.Core;
using DaCoder.DesktopClient.ViewModels;

namespace DaCoder.DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string pluginPath;

        public MainWindowViewModel MainWindowViewModel { get; set; }

        public RichTextBox RichTextBoxControl {
            get { return RichTextBox; }
        }

        public string PluginPath
        {
            get { return pluginPath; }
        }

        /// <summary>
        /// Import Classes
        /// </summary>
        [ImportMany(typeof (IPlugin))]
        public IEnumerable<Lazy<IPlugin>> GetIMain { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            pluginPath = AppDomain.CurrentDomain.BaseDirectory;
            pluginPath = pluginPath.Replace("\\Debug", "");
            pluginPath = pluginPath.Replace("\\Release", "");
            pluginPath = pluginPath.Replace("\\Bin", "");
            pluginPath = pluginPath + "\\plugins\\";
            InitializePlugins();
        }

        public void InitializePlugins()
        {
            try
            {
                // Getting all exported classes by default, there will be looked for *.dll files in the specified folder
                var catalog = new DirectoryCatalog(pluginPath);
                var container = new CompositionContainer(catalog);
                container.ComposeParts(this);

                var control = GetIMain.ToList()[0].Value as UserControl;

                if (control != null)
                    RootContainer.Children.Add(control);

                control.DataContext = this;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void RichTextBoxText_Changed(object sender, TextChangedEventArgs e)
        {
            if (DataContext.GetType() == typeof(MainWindowViewModel) && DataContext != null)
                MainWindowViewModel.RichTextBoxText = new TextRange(RichTextBoxControl.Document.ContentStart, RichTextBoxControl.Document.ContentEnd).Text;
        }
    }
}
