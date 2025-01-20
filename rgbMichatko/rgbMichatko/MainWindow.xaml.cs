using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace rgbMichatko
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isInitializing = true;
        public MainWindow()
        {
            InitializeComponent();
            _isInitializing = false;
        }

        private void ChangeColor()
        {
            int r = (int)sliRed.Value;
            int g = (int)sliGreen.Value;
            int b = (int)sliBlue.Value;

            txtRed.Text = r.ToString();
            txtGreen.Text = g.ToString();
            txtBlue.Text = b.ToString();

            lblHex.Content = $"#{r:X2}{g:X2}{b:X2}";
            rect.Fill = new SolidColorBrush(Color.FromRgb((byte)r, (byte)g, (byte)b));
            
        }

        private void txtRed_Enter(object sender, KeyEventArgs e)
        {
            if (_isInitializing || e.Key != Key.Enter) 
            {
                return;
            }
            if (double.TryParse(txtRed.Text, out double v))
            {
                if (v >= 0 && v <= 255)
                {
                    sliRed.Value = v;
                    ChangeColor();
                }
                else
                {
                    MessageBox.Show("Hodnota musí být mezi 0 a 255", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } 
        }

        private void txtGreen_Enter(object sender, KeyEventArgs e)
        {
            if (_isInitializing || e.Key != Key.Enter)
            {
                return;
            }
            if (double.TryParse(txtGreen.Text, out double v))
            {
                if (v >= 0 && v <= 255)
                {
                    sliGreen.Value = v;
                    ChangeColor();
                }
                else
                {
                    MessageBox.Show("Hodnota musí být mezi 0 a 255", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtBlue_Enter(object sender, KeyEventArgs e)
        {
            if (_isInitializing || e.Key != Key.Enter)
            {
                return;
            }
            if (double.TryParse(txtBlue.Text, out double v))
            {
                if (v >= 0 && v <= 255)
                {
                    sliBlue.Value = v;
                    ChangeColor();
                }
                else
                {
                    MessageBox.Show("Hodnota musí být mezi 0 a 255", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void sli_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ChangeColor();
        }

        private void txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(cc => Char.IsNumber(cc));
            base.OnPreviewTextInput(e);
        }
    }
}