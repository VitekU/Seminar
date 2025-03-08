using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Data.Sqlite;
using Microsoft.Win32;

namespace loginApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _source = "mainData";
        private string _selectedPath;
        private string _newPath;
        public MainWindow()
        {
            using (var connection = new SqliteConnection($"Data Source={_source}"))
            {
                connection.Open();

                string query = @"CREATE TABLE IF NOT EXISTS Users (Username TEXT PRIMARY KEY, Password TEXT NOT NULL, ImagePath TEXT NOT NULL, Quote TEXT NOT NULL);";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

            }
            InitializeComponent();
            MainFrame.Navigate(new LoginPage(_source, MainFrame));
        }

        private void fotka_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All files (*.*)|*.*",
                Title = "Vyberte si profilovou fotku."
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string newPath = Path.Combine(projectDirectory, "obrazky", Path.GetFileName(selectedImagePath));

                _newPath = newPath;
                _selectedPath = selectedImagePath;                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string quote = Quote.Text;

            if (username is string && password is string && quote is string && !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(quote))
            {
                using (var connection = new SqliteConnection($"Data Source={_source}"))
                {
                    connection.Open();

                    File.Copy(_selectedPath, _newPath, true);

                    string query = @"INSERT INTO Users (Username, Password, ImagePath, Quote) 
                                    VALUES (@username, @password, @imagePath, @quote)";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@imagePath", _newPath);
                        command.Parameters.AddWithValue("@quote", quote);


                        try 
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Úspěšně jste se zaregistrovali", "Databáze", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch 
                        { 
                            MessageBox.Show("Uživatel se stejným jménem už existuje.", "Databáze varování", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        
                    }

                }
            }
        }
    }
}