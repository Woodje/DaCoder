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
using DaCoder.Data;
using System.Windows.Media;

namespace DaCoder.DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Import Classes using IPlugin
        /// </summary>
        [ImportMany(typeof (IPlugin))]
        public IEnumerable<Lazy<IPlugin>> GetIMain { get; set; }

        private readonly string pluginPath;

        public MainWindowViewModel MainWindowViewModel { get; set; }

        public RichTextBox RichTextBoxControl {
            get { return RichTextBox; }
        }

        public string PluginPath
        {
            get { return pluginPath; }
        }

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
                    TopGrid.Children.Add(control);

                control.DataContext = this;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Event raised when a the text is cahnged.
        /// TODO: This should be converted to be done by pure binding instead using this event.
        /// TODO: No solution other than codebehind found!
        /// </summary>
        private void RichTextBoxText_Changed(object sender, TextChangedEventArgs e)
        {
            if (DataContext.GetType() == typeof(MainWindowViewModel) && DataContext != null)
                if (MainWindowViewModel.IsRichTextBoxTextAvailable)
                    MainWindowViewModel.RichTextBoxText = new TextRange(RichTextBoxControl.Document.ContentStart, RichTextBoxControl.Document.ContentEnd).Text;
        }

        /// <summary>
        /// Event raised when a menuitem has been clicked.
        /// TODO: This should be converted to be done by pure binding instead using this event.
        /// TODO: No solution other than codebehind found!
        /// </summary>
        private void MenuItem_Clicked(object sender, RoutedEventArgs e)
        {
            string selectedLanguage = ((MenuItem)sender).Header.ToString();

            foreach (var menuItem in LanguageMenuItems.Items)
            {
                if (((Language)menuItem).Name == selectedLanguage)
                {
                    if (DataContext.GetType() == typeof(MainWindowViewModel) && DataContext != null)
                    {
                        var language = MainWindowViewModel.SelectedLanguages.Find(l => l.Id == ((Language)menuItem).Id);
                        if (language == null)
                        {
                            MainWindowViewModel.SelectedLanguages.Add((Language)menuItem);
                            ((MenuItem)sender).IsChecked = true;
                        }
                        else
                        {
                            MainWindowViewModel.SelectedLanguages.Remove(language);
                            ((MenuItem)sender).IsChecked = false;
                        }
                    }
                }
            }
        }

    }
}
