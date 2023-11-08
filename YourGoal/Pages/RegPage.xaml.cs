using System.Windows;
using System.Windows.Controls;
using YourGoal.Services;

namespace YourGoal.Pages
{
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void RegButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы уже находитесь на этой странице!");
        }

        private void AcceptRegButton_OnClick(object sender, RoutedEventArgs e)
        {
            AllUserService userServices = new AllUserService();
            if (userServices.CreateNewUser(NameTextBox.Text, LoginTextBox.Text, PasswordTextBox.Text))
                NavigationService.Navigate(new AuthPage());
            else
                MessageBox.Show("Ошибка");
        }

        private void EnterButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }
    }
}