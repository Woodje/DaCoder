using System.Windows;

namespace DaCoder.DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for PluginInstallerDialog.xaml
    /// </summary>
    public partial class PluginInstallerDialog : Window
    {
        private readonly MainWindow mainWindow;

        /// <summary>
        /// Gets the underlying <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow MainWindow
        {
            get { return mainWindow; }
        }

        public PluginInstallerDialog(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }
    }
}
