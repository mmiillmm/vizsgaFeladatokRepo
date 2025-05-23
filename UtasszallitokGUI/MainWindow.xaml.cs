using System;
using System.Globalization;
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

namespace UtasszallitokGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            lstResults.Items.Clear();

            if (!double.TryParse(txtQc.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double qc) ||
                !double.TryParse(txtP0.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double p0) ||
                p0 <= 0)
            {
                MessageBox.Show("Nem megfelelő a bemeneti karakterlánc formátuma.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double exponent = 2.0 / 7.0;
            double mach = Math.Sqrt(5 * (Math.Pow(qc / p0 + 1, exponent) - 1));

            if (mach < 1)
            {
                lstResults.Items.Add($"qc={qc}, p0={p0}, Ma={mach:F15}");
            }
            else
            {
                lstResults.Items.Add("A Mach-szám nem szubszonikus (Ma >= 1)!");
            }
        }
    }
}