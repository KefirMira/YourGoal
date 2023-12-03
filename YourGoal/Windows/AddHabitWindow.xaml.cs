using System;
using System.Windows;
using YourGoal.Models;
using YourGoal.Services;

namespace YourGoal.Windows
{
    public partial class AddHabitWindow : Window
    {
        private User _user;
        public AddHabitWindow()
        {
            InitializeComponent();
        }
        public AddHabitWindow(User user)
        {
            InitializeComponent();
            _user = user;
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AllHabitAndTrackerService allHabit = new AllHabitAndTrackerService();
            if (NameTextBox.Text != "" && DaysTextBox.Text != "")
            {
                Habit newTask = new Habit();
                try
                {
                    if(FolderComboBox.SelectedItem!=null)
                        newTask.Folder = FolderComboBox.SelectedItem as Folder;
                    newTask.User = _user;
                    newTask.Name = NameTextBox.Text;
                    newTask.DateStart = DateTime.Now;
                    newTask.DateEnd = DateTime.Now.AddDays(Convert.ToInt32(DaysTextBox.Text));
                    if(allHabit.CreateNewHabit(newTask))
                        MessageBox.Show("Привычка успешно создана!");    
                }
                catch
                {
                    MessageBox.Show("Ошибка создания привычки!");
                    
                }
                
            }
        }

        private void AddHabitWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            AllFolderService allFolderService = new AllFolderService();
            FolderComboBox.ItemsSource = allFolderService.GetFolders();
        }
    }
}