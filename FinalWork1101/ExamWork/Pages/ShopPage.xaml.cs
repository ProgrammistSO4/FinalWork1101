using DataBaseLibrary;
using DataBaseLibrary.Models;
using DataBaseLibrary.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExamWork.Pages
{
    public partial class ShopPage : Page
    {
        private List<Product> Products { get; set; }

        private ProductService _service = new();

        public ShopPage()
        {
            InitializeComponent();

            SearchTextBox.TextChanged += Filters_Changed;
            minTextBox.TextChanged += Filters_Changed;
            maxTextBox.TextChanged += Filters_Changed;
            SortComboBox.SelectionChanged += Filters_Changed;
            ManufacturerComboBox.SelectionChanged += Filters_Changed;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NameLabel.Content = $"{CurrentUser.UserSurname} {CurrentUser.UserName} {CurrentUser.UserPatronymic}";

            if ((string)App.Current.Resources["UserName"] == "Гость")
            {
                ExitButton.Visibility = Visibility.Hidden;
                EnterButton.Visibility = Visibility.Visible;
            }
            else
            {
                ExitButton.Visibility = Visibility.Visible;
                EnterButton.Visibility = Visibility.Hidden;

            }

            await FillManufComboBoxAsync();
            await LoadProductAsync();
        }

        private async void Filters_Changed(object sender, RoutedEventArgs e)
        {
            ProductsStackPanel.Children.Clear();
            await LoadProductAsync();
        }

        private async Task LoadProductAsync()
        {
            decimal? minCost = null;
            decimal? maxCost = null;

            if (!string.IsNullOrWhiteSpace(minTextBox.Text) && decimal.TryParse(minTextBox.Text, out decimal parsedFromCost))
            {
                minCost = parsedFromCost;
            }
            if (!string.IsNullOrWhiteSpace(maxTextBox.Text) && decimal.TryParse(maxTextBox.Text, out decimal parsedToCost))
            {
                maxCost = parsedToCost;
            }
            Products = await _service.GetProductsAsync(SearchTextBox.Text, SortComboBox.SelectedIndex, minCost.GetValueOrDefault(), maxCost, ManufacturerComboBox.SelectedValue.ToString()
            );

            foreach (Product product in Products)
                AddProductContainer(product);
            countLabel.Content = $"Показано {Products.Count} из {await _service.GetProductsCountAsync()}";
        }

        public async Task FillManufComboBoxAsync()
        {
            var manufacturers = await _service.GetManufacturersAsync();

            if (manufacturers != null)
            {
                foreach (var manufacturer in manufacturers)
                {
                    ManufacturerComboBox.Items.Add(manufacturer);
                }
            }
        }

        private void AddProductContainer(Product productItem)
        {
            StackPanel panel = new()
            {
                Width = 630,
                Margin = new Thickness(15),
                Background = new SolidColorBrush(Color.FromRgb(200, 200, 200)),

            };

            Grid grid = new() { };
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            TextBlock ProductTextBlock = new TextBlock()
            {
                Text = productItem.Name,
                FontWeight = FontWeights.Bold
            };
            Grid.SetRow(ProductTextBlock, 0);
            Grid.SetColumn(ProductTextBlock, 0);
            grid.Children.Add(ProductTextBlock);

            TextBlock DescriptionTextBlock = new TextBlock
            {
                Text = productItem.Description
            };
            Grid.SetRow(DescriptionTextBlock, 1);
            Grid.SetColumn(DescriptionTextBlock, 0);
            grid.Children.Add(DescriptionTextBlock);

            TextBlock ManufacturerTextBlock = new TextBlock
            {
                Text = $"Производитель: {productItem.Manufacturer}",
            };
            Grid.SetRow(ManufacturerTextBlock, 2);
            Grid.SetColumn(ManufacturerTextBlock, 0);
            grid.Children.Add(ManufacturerTextBlock);

            TextBlock PriceTextBlock = new TextBlock
            {
                Text = $"Цена: {productItem.Cost}",
            };
            Grid.SetRow(PriceTextBlock, 3);
            Grid.SetColumn(PriceTextBlock, 0);
            grid.Children.Add(PriceTextBlock);

            Button OrderButton = new Button
            {
                Content = "Заказать",
                HorizontalAlignment = HorizontalAlignment.Right,
            };
            Grid.SetRow(OrderButton, 3);
            Grid.SetColumn(OrderButton, 1);
            grid.Children.Add(OrderButton);

            panel.Children.Add(grid);
            ProductsStackPanel.Children.Add(panel);

        }

        private void OrderAdminButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("еще на сделано");
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new CartPage());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new AuthorizationPage());
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new AuthorizationPage());
        }
    }
}