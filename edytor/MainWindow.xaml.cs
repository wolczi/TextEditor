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
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;


namespace edytor
{

    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        private bool hasTextChanged;
        private int newCounter = 0;
        private string path = "";

        public MainWindow()
        {
            InitializeComponent();

        }


        private void ShowToSaveMessage(string operation, object sender, ExecutedRoutedEventArgs e)
        {
            
            if (MessageBox.Show("Do you want to Save?", "Confirm",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Save_Executed(sender, e);
            }

            switch (operation)
            {
                case "New":
                    rtbEditor.SelectAll();
                    rtbEditor.Selection.Text = "";
                    Title = "New " + ++newCounter;
                    break;
                case "Exit":
                    Close();
                    break;
            }

        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (hasTextChanged)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";

                if (Title.StartsWith("C:"))
                {
                    FileStream fileStream = new FileStream(Title.Remove(Title.Length - 1), FileMode.Create);
                    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                    range.Save(fileStream, DataFormats.Text);
                    hasTextChanged = false;
                    Title = Title.Remove(Title.Length - 1);
                }
                else
                {
                    SaveAs_Executed(this, e);
                }

            }

            btnSave.IsChecked = false;
        }


        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (hasTextChanged)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";

                if (dlg.ShowDialog() == true)
                {
                        FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                        TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                        range.Save(fileStream, DataFormats.Text);
                        Title = dlg.FileName;
                        hasTextChanged = false;
                        path = dlg.FileName;
                }
            }

            btnSaveAs.IsChecked = false;
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Title.EndsWith("*"))
                e.CanExecute = true;
            else
                e.CanExecute = false;

        }

        private void SaveAs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Title.EndsWith("*"))
                e.CanExecute = true;
            else
                e.CanExecute = false;

        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (hasTextChanged)
            {
                ShowToSaveMessage("", this, e);
            }

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Text);
                Title = dlg.FileName;
                hasTextChanged = false;
                path=Title;
            }

            btnOpen.IsChecked = false;

        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (hasTextChanged)
                ShowToSaveMessage("Exit", this, e);
            else
                Close();

        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (hasTextChanged)
                ShowToSaveMessage("New", this, e);
            else
            {
                rtbEditor.SelectAll();
                rtbEditor.Selection.Text = "";
                Title = "New " + ++newCounter;
            }

            hasTextChanged = false;
            btnNew.IsChecked = false;
        }


        private void rtbEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            hasTextChanged = true;
            if (!Title.EndsWith("*"))
            {
                Title = Title.Insert(Title.Length, "*");
            }

        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Jakiś tekst linia 1 \n Potem jest linia druga \n I tak dalej . . . . . . . . . . . . . . . . . \n itd. \n . \n . \n .", "Okno Helpa", MessageBoxButton.OK, MessageBoxImage.Question);
            btnHelp.IsChecked = false;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ABOUT \n 1 \n Informatyka UWB \n 3 \n Programowanie w .NET \n 5 \n projekt notatnika", "Okno About", MessageBoxButton.OK, MessageBoxImage.Information);
            btnAbout.IsChecked = false;
        }

        private void btnCut_Click(object sender, RoutedEventArgs e)
        {
            btnCut.IsChecked = false;
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            btnCopy.IsChecked = false;
        }

        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            btnPaste.IsChecked = false;
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            btnUndo.IsChecked = false;
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            btnRedo.IsChecked = false;
        }


    }
}
