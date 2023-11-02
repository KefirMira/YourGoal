using System.Windows;
using System.Windows.Controls;
using YourGoal.Models;
using YourGoal.Services;

namespace YourGoal.Pages
{
    public partial class MainPage : Page
    {
        private User _user;
        public MainPage()
        {
            InitializeComponent();
        }
        public MainPage(User user)
        {
            InitializeComponent();
            _user = user;
        }

        private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            AllTaskServices taskServices = new AllTaskServices();
            AllTaskListView.ItemsSource = taskServices.GetAllTask(_user);
        }
    }
}