using DaCoder.Data;
using DaCoder.DesktopClient.ViewModels;
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
using System.Windows.Shapes;

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
