using DataBaseLibrary;
using DataBaseLibrary.Models;
using DataBaseLibrary.Services;
using System.Windows;
using System.Windows.Controls;

namespace ExamWork.Pages
{
    public partial class AuthorizationPage : Page
    {
        private UserService _service = new();

        public AuthorizationPage()
        {
            InitializeComponent();

            loginTextBox.Text = "loginDEmgu2018";
            passwordBox.Password = "0gC3bk";
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new();
            GetUserData(user);
            App.CurrentFrame.Navigate(new ShopPage());
        }

        private async void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (await _service.IsUserExistAsync(loginTextBox.Text, passwordBox.Password))
            {
                User user = await _service.GetUserAsync(loginTextBox.Text, passwordBox.Password);
                GetUserData(user);
                App.CurrentFrame.Navigate(new ShopPage());
            }
            else
            {
                MessageBox.Show("Логин или пароль введены неверно.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GetUserData(User user)
        {
            CurrentUser.UserID = user.UserId;
            CurrentUser.RoleID = user.RoleId;
            CurrentUser.UserName = user.Name;
            CurrentUser.UserSurname = user.Surname;
            CurrentUser.UserPatronymic = user.Patronymic;
            CurrentUser.UserLogin = user.Login;
            CurrentUser.UserPassword = user.Password;
        }
    }
}
