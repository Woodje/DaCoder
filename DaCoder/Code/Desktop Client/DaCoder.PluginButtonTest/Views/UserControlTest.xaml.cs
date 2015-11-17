using DaCoder.Core;
using DaCoder.DesktopClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DaCoder.PluginButtonTest.Views
{
    /// <summary>
    /// Interaction logic for UserControlTest.xaml
    /// </summary>
    public partial class UserControlTest : UserControl, IPlugin
    {
        public UserControlTest()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This comes from plugin 'DaCoder.UserControlTest'");

            if (DataContext.GetType() == typeof(MainWindow))
                ((MainWindow) DataContext).RichTextBoxControl.AppendText("koen");
        }

    }
}
