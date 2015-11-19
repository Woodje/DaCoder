using DaCoder.CommentPlugin.ViewModels;
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

namespace DaCoder.CommentPlugin.Views
{
    /// <summary>
    /// Interaction logic for CommentPluginToolbar.xaml
    /// </summary>
    public partial class CommentPluginToolbar : UserControl, IPlugin
    {
        public CommentPluginToolbar()
        {
            InitializeComponent();
        }

        private void CommentOutCode_Click(object sender, RoutedEventArgs e)
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

                    // Remove the comment from the last line
                    if (text.EndsWith(comment))
                        text = text.Remove(text.Length - comment.Length);

                    richtextbox.Selection.Text = text;
                }
                else
                {
                    richtextbox.CaretPosition.GetLineStartPosition(0).InsertTextInRun("// ");
                }
            }
        }

        private void RemoveComments_Click(object sender, RoutedEventArgs e)
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
