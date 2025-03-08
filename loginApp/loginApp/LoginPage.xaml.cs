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
    /// Interakční logika pro LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private string _mainData;
        private Frame _mFrame;
        public LoginPage(string mn, Frame mFrame)
        {
            _mainData = mn;
            _mFrame = mFrame;
            InitializeComponent();
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            using (var connection = new SqliteConnection($"Data Source={_mainData}"))
            {
                connection.Open();
                string getUsers = "SELECT Username, Password FROM Users";
                var command = new SqliteCommand(getUsers, connection);
                using (var reader = command.ExecuteReader())
                {
                    string count = "SELECT COUNT(*) FROM Users";
                    using (var com = new SqliteCommand(count, connection))
                    {
                        var rowCount = com.ExecuteScalar();
                        if ((long)rowCount == 0)
                        {
                            OutText.Text = "Uživatel neexistuje.";
                        }
                    }

                    
                    while (reader.Read())
                    {
                        string uname = reader.GetString(0);
                        string pass = reader.GetString(1);
                        if (username == uname)
                        {
                            if (password == pass)
                            {
                                _mFrame.Navigate(new UserDisplay(_mFrame, _mainData, uname));
                            }
                            else
                            {
                                OutText.Text = "Špatné heslo.";
                                break;
                            }
                        }
                        else
                        {
                            OutText.Text = "Uživatel neexistuje.";
                        }
                    }
                }
            }
        }
    }
}
