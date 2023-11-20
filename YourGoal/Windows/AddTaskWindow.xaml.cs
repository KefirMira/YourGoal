using System;
using System.Windows;
using YourGoal.Models;
using YourGoal.Services;

namespace YourGoal.Windows
{
    public partial class AddTaskWindow : Window
    {
         User _user;
        public AddTaskWindow()
        {
            InitializeComponent();
        }
        public AddTaskWindow(User user)
        {
            InitializeComponent();
            _user = user;
        }
        

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AllTaskServices allTaskServices = new AllTaskServices();
            if (NameTextBox.Text != "" && DateDeleteTextBox.Text != "" && DateDeleteTextBox.SelectedDate < DateTime.Now)
            {
                Task newTask = new Task();
                try
                {
                    if(FolderComboBox.SelectedItem!=null)
                        newTask.Folder = FolderComboBox.SelectedItem as Folder;
                    if(PriorityComboBox.SelectedItem!=null)
                        newTask.Priority = PriorityComboBox.SelectedItem as Priority;
                    newTask.User = _user;
                    newTask.Name = NameTextBox.Text;
                    newTask.DateDelete = Convert.ToDateTime(DateDeleteTextBox.SelectedDate);
                    allTaskServices.AddNewTask(newTask);
                }
                catch
                {
                    MessageBox.Show("Ошибка создания задачи!");
                }
                
            }
            
        }

        private void AddTaskWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            AllPriorityService allPriorityService = new AllPriorityService();
            AllFolderService allFolderService = new AllFolderService();
            PriorityComboBox.ItemsSource = allPriorityService.GetFolders();
            FolderComboBox.ItemsSource = allFolderService.GetFolders();
        }
    }
}