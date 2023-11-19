using System.Windows;
using System.Windows.Controls;
using YourGoal.Models;

namespace YourGoal.Pages
{
    public partial class TasksPage : Page
    {
        public TasksPage()
        {
            InitializeComponent();
        }
        
        public TasksPage(User user)
        {
            InitializeComponent();
        }

        private void GoMainBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}