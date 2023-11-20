using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Xceed.Document.NET;
using Xceed.Words.NET;
using YourGoal.Models;
using YourGoal.Services;
using YourGoal.Windows;

namespace YourGoal.Pages
{
    public partial class TasksPage : Page
    {
        User _user;
        public TasksPage()
        {
            InitializeComponent();
        }
        
        public TasksPage(User user)
        {
            InitializeComponent();
            _user = user;
        }

        private void GoMainBtn_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(_user));
        }

        private void AddFolderButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddFolderWindow addFolderWindow = new AddFolderWindow(_user);
            addFolderWindow.ShowDialog();
            this.IsEnabled = false;
            addFolderWindow.Close();
            this.IsEnabled = true;
            Lode();
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow(_user);
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
        
        public void Lode()
        {
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

        private void PrintButton_OnClick(object sender, RoutedEventArgs e)
        {
            AllTaskServices allTaskServices = new AllTaskServices();
            List<Task> tasks = allTaskServices.GetAllTask(_user);
            string path = @"..\..\Documents\"+ _user.Login+DateTime.Now.ToString("dd.MM.yyyy")+".docx";
        
            DocX document = DocX.Create(path);
            foreach (var item in tasks)
            {
                Paragraph paragraph = document.InsertParagraph();
                paragraph.AppendLine($"Наименование - {item.Name}\n Категория - {item.Folder.Name}\n Приоритет - {item.Priority.Name}\n Дата окончания - {item.DateDelete}")
                    .Font("Times New Roman")
                    .FontSize(12)
                    .Alignment = Alignment.center;
            }
            document.Save();
        }

        private void AllTaskListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Task selectedTask = AllTaskListView.SelectedItem as Task;
            AddTaskWindow addTaskWindow = new AddTaskWindow(_user,selectedTask);
            addTaskWindow.ShowDialog();
            this.IsEnabled = false;
            addTaskWindow.Close();
            this.IsEnabled = true;
            Lode();
        }
    }
}