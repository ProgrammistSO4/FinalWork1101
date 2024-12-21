using DataBaseLibrary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExamWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NameLabel.Content = $"{CurrentUser.UserSurname} {CurrentUser.UserName} {CurrentUser.UserPatronymic}";
        }

      

        private void OldersListButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new OrderPage());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new ShopPage());
        }
    }
}
