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
using System.Linq;
using System.IO;
using System.Globalization;

namespace LaptopsGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Laptop> laptops;

        public MainWindow()
        {
            InitializeComponent();

            laptops = LoadLaptops();

            int manufacturerCount = laptops
                .Select(l => l.Manufacturer)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct()
                .Count();

            int laptopCount = laptops.Count;
            string summary = $"Összesen {manufacturerCount} gyártó {laptopCount} gépe közül lehet választani.";

            var items = new List<string> { summary };
            items.AddRange(laptops.Select((laptop, index) => $"{index + 1}. {laptop}"));

            laptopListBox.ItemsSource = items;
            laptopListBox.SelectedIndex = 0;

            laptopListBox.SelectionChanged += LaptopListBox_SelectionChanged;
            LaptopTextBox.Text = "";
        }

        private void LaptopListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = laptopListBox.SelectedIndex - 1;
            if (index < 0 || index >= laptops.Count)
            {
                LaptopTextBox.Text = "";
                return;
            }

            var laptop = laptops[index];
            LaptopTextBox.Text = GetLaptopDetailsText(laptop);
        }

        private string GetLaptopDetailsText(Laptop laptop)
        {
            string screen = laptop.Screen ?? "";
            string diagonal = "";
            string resolution = "";

            int quoteIndex = screen.IndexOf('"');
            if (quoteIndex > 0)
            {
                diagonal = screen.Substring(0, quoteIndex + 1).Trim();
                int parenStart = screen.IndexOf('(');
                int parenEnd = screen.IndexOf(')');
                if (parenStart > 0 && parenEnd > parenStart)
                {
                    resolution = screen.Substring(parenStart + 1, parenEnd - parenStart - 1).Trim();
                }
            }

            int priceHuf = (int)Math.Round(laptop.Price * 4.12);

            var sb = new StringBuilder();
            sb.AppendLine($"Gyártó: {laptop.Manufacturer}");
            sb.AppendLine($"Típus: {laptop.Model}");
            sb.AppendLine($"Kategória: {laptop.Category?.CategoryName}");
            sb.AppendLine($"CPU: {laptop.CPU}");
            sb.AppendLine($"RAM: {laptop.RAM}");
            sb.AppendLine($"Képátló: {diagonal}");
            sb.AppendLine($"Felbontás: {resolution}");
            sb.AppendLine($"OS: {laptop.OS}");
            sb.AppendLine($"Tárhely: {laptop.Storage}");
            sb.AppendLine($"Ár: {priceHuf} Ft");

            return sb.ToString();
        }

        private List<Laptop> LoadLaptops()
        {
            var laptops = new List<Laptop>();
            string filePath = @"..\..\..\src\laptops.txt";

            if (!File.Exists(filePath))
                return laptops;

            foreach (var line in File.ReadLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(';');
                if (parts.Length < 9)
                    continue;

                var category = new Category { CategoryName = parts[0] };

                int.TryParse(parts[5], NumberStyles.Any, CultureInfo.InvariantCulture, out int price);

                laptops.Add(new Laptop(
                    category,
                    parts[1],
                    parts[2],
                    parts[3],
                    parts[4],
                    price,
                    parts[6],
                    parts[7],
                    parts[8]
                ));
            }

            return laptops;
        }
    }
}