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
using Microsoft.Data.Sqlite;

namespace loginApp
{
    /// <summary>
    /// Interakční logika pro UserDisplay.xaml
    /// </summary>
    public partial class UserDisplay : Page
    {
        private Frame _mFrame;
        private string _mainData;
        private string _username;
        public UserDisplay(Frame mFrame, string mainData, string username)
        {
            _mFrame = mFrame;
            _mainData = mainData;
            _username = username;
            InitializeComponent();

            Username.Text = username;

            using (var connection = new SqliteConnection($"Data Source={_mainData}"))
            {
                connection.Open();
                string getData = $"SELECT ImagePath, Quote FROM Users WHERE Username = '{_username}'";
                var command = new SqliteCommand(getData, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string path = reader.GetString(0);
                        string quote = reader.GetString(1);


                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri(path); // Set the image path
                        bitmapImage.EndInit();

                        profilePicture.Source = bitmapImage;
                        Quote.Text = quote;
                    }
                }
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mFrame.Navigate(new LoginPage(_mainData, _mFrame));
        }

    }
}
