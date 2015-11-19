using DaCoder.Core;
using DaCoder.DesktopClient.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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

        /// <summary>
        /// Comment out the current line or the selected text
        /// </summary>
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
                else if (richtextbox.IsFocused)
                {
                    richtextbox.CaretPosition.GetLineStartPosition(0).InsertTextInRun("// ");
                }
            }
        }

        /// <summary>
        /// Remove comments from the selected text
        /// </summary>
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
