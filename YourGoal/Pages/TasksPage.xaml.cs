using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using YourGoal.Models;
using YourGoal.Services;
using YourGoal.Windows;

namespace YourGoal.Pages
{
    public partial class TasksPage : Page
    {
        private User _user;
        public TasksPage()
        {
            InitializeComponent();
        }
        
        public TasksPage(User user)
        {
            InitializeComponent();
            _user = new User();
        }

        private void GoMainBtn_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(_user));
        }

        private void AddFolderButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddFolderWindow addFolderWindow = new AddFolderWindow();
            addFolderWindow.ShowDialog();
            this.IsEnabled = false;
            addFolderWindow.Close();
            this.IsEnabled = true;
            Lode();
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow();
            addTaskWindow.ShowDialog();
            this.IsEnabled = false;
            addTaskWindow.Close();
            this.IsEnabled = true;
            Lode();
        }

        private void TasksPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Lode();
        }
        
        public void Lode(){
            AllTaskServices allTaskServices = new AllTaskServices();
            AllFolderService allFolderService = new AllFolderService();
            List<Task> tasks = allTaskServices.GetAllTask(_user);
            List<Folder> folders = allFolderService.GetFolders();
            AllTaskListView.ItemsSource = tasks;
            FolderListView.ItemsSource = folders;
            
        }
        
        private void FolderListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}