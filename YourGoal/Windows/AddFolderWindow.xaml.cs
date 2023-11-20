using System.Windows;
using YourGoal.Services;

namespace YourGoal.Windows
{
    public partial class AddFolderWindow : Window
    {
        public AddFolderWindow()
        {
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AllFolderService allFolderService = new AllFolderService();
            if (allFolderService.CreateNewFolder(NameTextBox.Text))
                MessageBox.Show("Папка добавлена");
            else
                MessageBox.Show("Ошибка!");
        }
    }
}