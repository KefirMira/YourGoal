using System.Windows.Controls;
using YourGoal.Models;

namespace YourGoal.Pages
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public MainPage(User user)
        {
            InitializeComponent();
        }
    }
}