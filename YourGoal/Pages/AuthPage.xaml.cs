using System.Threading;
using System.Windows;
using System.Windows.Controls;
using YourGoal.Models;
using YourGoal.Services;

namespace YourGoal.Pages
{
    public partial class AuthPage : Page
    {
        private int i;
        public AuthPage()
        {
            InitializeComponent();
            i = 0;
        }

        private void ForgotYourPasswordButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PasswordRecoveryPage());
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            string Login = LoginTextBox.Text;
            string Password = PasswordTextBox.Text;
            AllUserService au = new AllUserService();
            User user = au.AuthUser(Login, Password);
            if (user.Id == 0)
            {
                WarningTextBlock.Text = "Ошибка! Неверный логин или пароль!"; 
                i++;
                if (i % 3==0)
                {
                    LoginButton.IsEnabled = false;
                    MessageBox.Show("Ошибка! Неверный логин или пароль!\n Авторизация невозможна!");
                    Thread.Sleep(10000);
                }
                WarningTextBlock.Text = "Ошибка!  Неверный логин или пароль!"; 
            }
            else
            {
                NavigationService.Navigate(new MainPage(user));
            }
        }

        private void EnterButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы уже находитесь на этой странице!");
        }

        private void RegButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }
    }
}