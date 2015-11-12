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
using DaCoder.Core;

namespace DaCoder.PluginButtonTest
{
    /// <summary>
    /// Interaction logic for UserControlButtonTest.xaml
    /// </summary>
    public partial class UserControlButtonTest : UserControl, IPlugin
    {
        public UserControlButtonTest()
        {
            InitializeComponent();
        }

        public string NameOfUserControl
        {
            get { return "UserControlButtonTest"; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This comes from plugin 'DaCoder.PluginButtonTest'");
        }
    }
}
