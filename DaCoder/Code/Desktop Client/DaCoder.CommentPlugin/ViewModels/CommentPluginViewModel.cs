using DaCoder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaCoder.CommentPlugin.Views;
using DaCoder.DesktopClient.Views;
using System.Windows.Documents;

namespace DaCoder.CommentPlugin.ViewModels
{
    public class CommentPluginViewModel : ViewModel
    {
        private CommentPluginToolbar commentPluginToolbar;

        public CommentPluginViewModel(CommentPluginToolbar commentPluginToolbar)
        {
            this.commentPluginToolbar = commentPluginToolbar;
        }


        public ActionCommand CommentOutCodeCommand
        {
            get
            {
                return new ActionCommand(parameter => CommentOutCode());
            }
        }

        private void CommentOutCode()
        {
            if (commentPluginToolbar.DataContext.GetType() == typeof(MainWindow))
            {
                var richtextbox = ((MainWindow)commentPluginToolbar.DataContext).RichTextBoxControl;

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
    }
}
