using System;
using System.Windows;
using YourGoal.Models;
using YourGoal.Services;

namespace YourGoal.Windows
{
    public partial class AddTaskWindow : Window
    {
         User _user;
         private Task _task;
        public AddTaskWindow()
        {
            InitializeComponent();
        }
        public AddTaskWindow(User user)
        {
            InitializeComponent();
            _user = user;
            _task = new Task();
        }
        //апедейт
        public AddTaskWindow(User user, Task task)
        {
            InitializeComponent();
            _user = user;
            _task = task;
            this.DataContext = _task;
        }
        

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AllTaskServices allTaskServices = new AllTaskServices();

            if (_task.Id == 0)
            {
                if (NameTextBox.Text != "" && DateDeleteTextBox.Text != "" && DateDeleteTextBox.SelectedDate > DateTime.Now)
                {
                    Task newTask = new Task();
                    try
                    {
                        if(FolderComboBox.SelectedItem!=null)
                            newTask.Folder = FolderComboBox.SelectedItem as Folder;
                        if(PriorityComboBox.SelectedItem!=null)
                            newTask.Priority = PriorityComboBox.SelectedItem as Priority;
                        newTask.User = _user;
                        newTask.Accomplishment = false;
                        newTask.Name = NameTextBox.Text;
                        newTask.DateDelete = Convert.ToDateTime(DateDeleteTextBox.SelectedDate);
                        allTaskServices.AddNewTask(newTask);
                        MessageBox.Show("Успешно!");
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка создания задачи!");
                    }
                }
            }
            else
            {
                if (allTaskServices.ChangeTask(_task))
                    MessageBox.Show("Успешно!");
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