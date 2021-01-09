using BookApp.DAL;
using BookApp.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdminDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBookRepository bookRepository;
        public MainWindow(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
            InitializeComponent();
            GetBooks();
        }

        private void GetBooks()
        {
            var books =  bookRepository.GetBooks();
            BooksGrid.ItemsSource = books;
        }

        private void BooksGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var book = e.Row.DataContext as Book;

            if (book == null)
                return;

            bookRepository.UpsertBook(book);
            GetBooks();
            MessageBox.Show(book.Title + " has been upserted sucessfully.", "Add/Update Book", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private  void BooksGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var grid = (DataGrid)sender;
                if (grid.SelectedItems.Count <= 0)
                    return;

                if (grid.SelectedItems.Count>1)
                {
                    MessageBox.Show("Only 1 book record can be deleted at a time", "Deleting Book(s)", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                var result = MessageBox.Show("Are you sure you want to delete selected book(s)?", "Deleting Book(s)", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    var book = grid.SelectedItem as Book;
                    if (book!=null && book.Id > 0)
                         bookRepository.DeleteBook(book.Id);
                }

                 GetBooks();
            }
        }

        private void buttonBrowse_Click(object sender, RoutedEventArgs e)
        {
            var OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Title = "Choose Cover Image";
            OpenFileDialog.Filter = "JPEG (*.jpg;*.jpeg;*.jpe)|*.jpg;*.jpeg;*.jpe|PNG (*.png)|*.png|All Files (*.*)|*.*";
            if (OpenFileDialog.ShowDialog() == true)
            {
                var clipBoardData = Clipboard.GetDataObject();
                var book = ((FrameworkElement)sender).DataContext as Book;

                if (book != null && book.Id>0)
                {
                    book.CoverImage = GetPhoto(OpenFileDialog.FileName);
                    bookRepository.UpsertBook(book);

                    GetBooks();
                    MessageBox.Show("Cover Image uploaded sucessfully!", "Cover Image Upload", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Can't uploaded Cover Image, Please Add the book first!", "Add Book", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
        }

        public static byte[] GetPhoto(string filePath)
        {
            FileStream stream = new FileStream(
            filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;
        }

        private void buttonExport_Click(object sender, RoutedEventArgs e)
        {
            var book = ((FrameworkElement)sender).DataContext as Book;

            if (book == null)
            {
                MessageBox.Show("Select a valid book to be exported", "Book Export",MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.Title = "Book Export to Json";
            SaveFileDialog.Filter = "JSON (*.json)|*.json|All Files (*.*)|*.*";
            SaveFileDialog.FileName = book.Title;
            SaveFileDialog.ShowDialog();
            if (SaveFileDialog.FileName !="")
            {
                using (StreamWriter file = File.CreateText(SaveFileDialog.FileName))
                {
                    var serializedJson=JsonConvert.SerializeObject(book, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                    file.Write(serializedJson);
                    file.Close();
                }

                    MessageBox.Show("Book exported sucessfully!", "Book Export", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }
    }
}
