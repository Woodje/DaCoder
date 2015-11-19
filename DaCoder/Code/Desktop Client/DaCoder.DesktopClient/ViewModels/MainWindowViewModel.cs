using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using DaCoder.Core;
using DaCoder.DesktopClient.Views;
using System.Collections.Generic;
using DaCoder.Data;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Microsoft.Win32;

namespace DaCoder.DesktopClient.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly MainWindow mainWindow;

        public ICollection<Language> Languages { get; set; }

        public List<Language> SelectedLanguages { get; set; }

        public RichTextBox RichTextBoxControl { get; set; }

        public bool IsRichTextBoxTextAvailable { get; private set; }

        private string richTextBoxText;

        public string RichTextBoxText
        {
            get { return richTextBoxText; }
            set
            {
                richTextBoxText = value;
                UseLanguage();
            }
        }

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            RichTextBoxControl = mainWindow.RichTextBoxControl;

            Languages = new ObservableCollection<Language>();

            SelectedLanguages = new List<Language>();

            IsRichTextBoxTextAvailable = true;

            using (var businessContext = new BusinessContext())
            {
                var queryAllLanguages =
                    from languages in businessContext.DataContext.Languages
                    select languages;

                foreach (var language in queryAllLanguages)
                {
                    Languages.Add(language);
                }
            }
        }

        /// <summary>
        /// Gets the command that makes it possible to use a language.
        /// </summary>
        public ActionCommand UseLanguageCommand
        {
            get { return new ActionCommand(parameter => UseLanguage()); }
        }

        /// <summary>
        /// Uses the language.
        /// </summary>
        private void UseLanguage()
        {
            IsRichTextBoxTextAvailable = false;

            var searchInTextRange = new TextRange(RichTextBoxControl.Document.ContentStart, RichTextBoxControl.Document.ContentEnd);

            searchInTextRange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));


            using (var businessContext = new BusinessContext())
            {
                foreach (var selectedLanguage in businessContext.DataContext.Languages)
                {
                    var language = SelectedLanguages.Find(l => l.Id == selectedLanguage.Id);
                    if (language != null)
                    {
                        foreach (var keyword in selectedLanguage.Keywords)
                        {
                            TextRange foundInTextRange = FindTextFromTextPointerPosition(RichTextBoxControl.Document.ContentStart, keyword.Name);

                            while (foundInTextRange != null)
                            {
                                foundInTextRange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));
                                RichTextBoxControl.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
                                foundInTextRange = FindTextFromTextPointerPosition(foundInTextRange.End, keyword.Name);
                            }
                        }
                    }
                }
            }

            IsRichTextBoxTextAvailable = true;
        }

        TextRange FindTextFromTextPointerPosition(TextPointer textPointerPosition, string text)
        {
            while (textPointerPosition != null)
            {
                if (textPointerPosition.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = textPointerPosition.GetTextInRun(LogicalDirection.Forward);

                    int indexInRun = textRun.IndexOf(text);
                    if (indexInRun >= 0)
                    {
                        TextPointer textPointerStartPosition = textPointerPosition.GetPositionAtOffset(indexInRun);
                        TextPointer textPointerEndPosition = textPointerStartPosition.GetPositionAtOffset(text.Length);
                        return new TextRange(textPointerStartPosition, textPointerEndPosition);
                    }
                }

                textPointerPosition = textPointerPosition.GetNextContextPosition(LogicalDirection.Forward);
            }

            return null;
        }

        /// <summary>
        /// Gets the command that makes it possible to add a new plugin.
        /// </summary>
        public ActionCommand AddPluginDialogCommand
        {
            get { return new ActionCommand(parameter => AddPluginDialog()); }
        }

        /// <summary>
        /// Opens up the plugin dialog.
        /// </summary>
        private void AddPluginDialog()
        {
            var pluginInstallerDialog = new PluginInstallerDialog(mainWindow);
            pluginInstallerDialog.DataContext = new PluginInstallerViewModel(pluginInstallerDialog);
            pluginInstallerDialog.ShowDialog();
        }

        /// <summary>
        /// Gets the command that makes it possible to edit the languages.
        /// </summary>
        public ActionCommand LanguageOptionDialogCommand
        {
            get { return new ActionCommand(parameter => LanguageOptionDialog()); }
        }

        /// <summary>
        /// Opens up the language options dialog.
        /// </summary>
        private void LanguageOptionDialog()
        {
            var languageOptionDialog = new LanguageOptionDialog();
            languageOptionDialog.DataContext = new LanguageOptionViewModel();
            languageOptionDialog.ShowDialog();
        }

        /// <summary>
        /// Gets the command that opens the save file dialog.
        /// </summary>
        public ActionCommand SaveFileAsCommand
        {
            get { return new ActionCommand(parameter => SaveFileAs()); }
        }

        /// <summary>
        /// Opens up the save file dialog.
        /// </summary>
        private void SaveFileAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Show All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, RichTextBoxText);
        }

        /// <summary>
        /// Gets the command that shutdown the program.
        /// </summary>
        public ActionCommand ExitCommand
        {
            get { return new ActionCommand(parameter => Exit()); }
        }

        /// <summary>
        /// Shutdown the program.
        /// </summary>
        private void Exit()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Gets the command that opens the open file dialog.
        /// </summary>
        public ActionCommand OpenCommand
        {
            get { return new ActionCommand(parameter => Open()); }
        }

        /// <summary>
        /// Opens up the open file dialog.
        /// </summary>
        private void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    RichTextBoxControl.AppendText(File.ReadAllText(openFileDialog.FileName));
                }
                catch
                {
                    MessageBox.Show("File type not supported");
                }
            }
        }

    }
}