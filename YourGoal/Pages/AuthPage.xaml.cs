using System.Windows;
using System.Windows.Controls;
using YourGoal.Models;
using YourGoal.Services;

namespace YourGoal.Pages
{
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void ForgotYourPasswordButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            string Login = LoginTextBox.Text;
            string Password = PasswordTextBox.Text;
            AuthService au = new AuthService();
            User user = au.AuthUser(Login, Password);
            if (user == null)
            {
                MessageBox.Show("Ошибка");
            }
            else
            {
                MessageBox.Show("!!");
            }
        }

        private void EnterButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void RegButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}