using System;
using System.Windows;
using YourGoal.Models;
using YourGoal.Services;

namespace YourGoal.Windows
{
    public partial class AddGoalWindow : Window
    {
        User _user;
        Goal _goal;
        public AddGoalWindow()
        {
            InitializeComponent();
        }
        public AddGoalWindow(User user )
        {
            InitializeComponent();
            _user = user;
        }
        
        //апдейт
        public AddGoalWindow(User user,Goal goal )
        {
            InitializeComponent();
            _user = user;
            _goal = goal;
        }


        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AllGoalServices allGoalServices = new AllGoalServices();
            if (NameTextBox.Text != "" && DateEndTextBox.Text != ""&& DateStartTextBox.Text != "" && DateStartTextBox.SelectedDate<DateEndTextBox.SelectedDate)
            {
                Goal newGoal = new Goal();
                try
                {
                    if(FolderComboBox.SelectedItem!=null)
                        newGoal.Folder = FolderComboBox.SelectedItem as Folder;
                    newGoal.DateStart = Convert.ToDateTime(DateStartTextBox.SelectedDate);
                    newGoal.Name = NameTextBox.Text;
                    newGoal.DateEnd = Convert.ToDateTime(DateEndTextBox.SelectedDate);
                    allGoalServices.AddNewGoal(newGoal);
                    MessageBox.Show("Успешно!");
                }
                catch
                {
                    MessageBox.Show("Ошибка создания задачи!");
                }
                
            }
        }

        private void AddGoalWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            AllFolderService allFolderService = new AllFolderService();
            FolderComboBox.ItemsSource = allFolderService.GetFolders();
        }
    }
}