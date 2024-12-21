using DataBaseLibrary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExamWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NameLabel.Content = $"{CurrentUser.UserSurname} {CurrentUser.UserName} {CurrentUser.UserPatronymic}";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new CartPage());
        }
    }
}
