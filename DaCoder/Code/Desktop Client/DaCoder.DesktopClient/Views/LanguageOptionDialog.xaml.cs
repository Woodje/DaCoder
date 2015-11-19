using DaCoder.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DaCoder.DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for LanguageOptionDialog.xaml
    /// </summary>
    public partial class LanguageOptionDialog : Window
    {
        public LanguageOptionDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event raised when a cell has finished editing.
        /// The edited cell is either updated or added to the database.
        /// TODO: This should be converted to be done by pure binding instead using this event.
        /// TODO: USE: xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity" for binding!
        /// TODO: USE: xmlns:ei="http://schemas.microsoft.com/expression/2010/interactions" for binding!
        /// </summary>
        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            using (var businessContext = new BusinessContext())
            {
                Keyword keyword = (Keyword) e.Row.Item;
                keyword = businessContext.DataContext.Keywords.Find(keyword.Id);

                if (keyword != null)
                {
                    keyword.Name = ((TextBox)e.EditingElement).Text;
                    businessContext.UpdateKeyword(keyword);
                }
                else
                {
                    keyword = new Keyword
                    {
                        Name = ((TextBox)e.EditingElement).Text,
                        LanguageId = ((Language)LanguageTabControl.SelectedValue).Id
                    };

                    businessContext.AddNewKeyword(keyword);
                }
            }
        }

        /// <summary>
        /// Event raised when key is pressed on a datagrid.
        /// Checks if a cell was deleted.
        /// If it was deleted then update the database.
        /// TODO: This should be converted to be done by pure binding instead using this event.
        /// TODO: USE: xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity" for binding!
        /// TODO: USE: xmlns:ei="http://schemas.microsoft.com/expression/2010/interactions" for binding!
        /// </summary>
        private void KeywordsDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Delete == e.Key)
            {
                foreach (var row in ((DataGrid)sender).SelectedItems)
                {
                    if (!row.ToString().Equals("{NewItemPlaceholder}"))
                    {
                        using (var businessContext = new BusinessContext())
                        {
                            Keyword keyword = (Keyword)row;

                            businessContext.DeleteKeyword(keyword);
                        }
                    }
                }
            }
        }
    }
}
