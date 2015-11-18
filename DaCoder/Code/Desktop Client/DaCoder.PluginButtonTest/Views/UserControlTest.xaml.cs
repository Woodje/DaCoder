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
            if (DataContext.GetType() == typeof(MainWindow))
            {
                var richtextbox = ((MainWindow)DataContext).RichTextBoxControl;

                if (!richtextbox.Selection.IsEmpty)
                {
                    string commentText = "//";

                    string comment = Environment.NewLine + commentText;

                    richtextbox.Selection.Start.InsertTextInRun(commentText);

                    TextRange textRange = new TextRange(richtextbox.Selection.Start, richtextbox.Selection.End);

                    string text = textRange.Text.Replace(Environment.NewLine, comment);

                    if (text.EndsWith(comment))
                        text = text.Remove(text.Length - comment.Length);

                    richtextbox.Selection.Text = text;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataContext.GetType() == typeof(MainWindow))
            {
                var richtextbox = ((MainWindow)DataContext).RichTextBoxControl;
                if (!richtextbox.Selection.IsEmpty)
                {
                    string commentText = "//";

                    string comment = Environment.NewLine + commentText;

                    richtextbox.Selection.Start.InsertTextInRun(commentText);

                    TextRange textRange = new TextRange(richtextbox.Selection.Start, richtextbox.Selection.End);

                    string text = textRange.Text.Replace(commentText, null);

                    richtextbox.Selection.Text = text;
                }
            }
        }


    }
}
