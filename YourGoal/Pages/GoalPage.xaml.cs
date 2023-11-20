using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using YourGoal.Models;
using YourGoal.Services;
using YourGoal.Windows;

namespace YourGoal.Pages
{
    public partial class GoalPage : Page
    {
        private User _user;
        public GoalPage()
        {
            InitializeComponent();
        }
        public GoalPage(User user)
        {
            InitializeComponent();
            _user = user;
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
        
        public void Lode()
        {
            AllFolderService allFolderService = new AllFolderService();
            List<Folder> folders = allFolderService.GetFolders();
            FolderListView.ItemsSource = folders;
            AllGoalListView.ItemsSource = GetGoals().Distinct();
        }

        public List<Goal> GetGoals()
        {
            List<TaskOnGoal> taskOnGoals = new List<TaskOnGoal>();
            TaskForGoalService taskForGoalService = new TaskForGoalService();
            taskOnGoals = taskForGoalService.GetAllTaskAndGoal(_user);
            List<Goal> goal = new List<Goal>();
            foreach (var item in taskOnGoals)
            {
                Goal thgoal = new Goal();
                thgoal = item.Goal;
                thgoal.RelationshipTasks = RelationshipTasksCounter(taskOnGoals, item.Goal);
                int count = thgoal.DateStart.Day - thgoal.DateEnd.Day;
                thgoal.RemainingDays = count;
                thgoal.AllTasks = GetAllTasks(taskOnGoals, thgoal);
                thgoal.ReadyTasks = GetReadyTasks(taskOnGoals, thgoal);
                goal.Add(thgoal);
            }

            return goal;
        }
        
        public string RelationshipTasksCounter(List<TaskOnGoal> taskOnGoals, Goal thisGoal )
        {
            int allTasks = 0;
            int readyTasks = 0;
            foreach (var item in taskOnGoals)
            {
                if (item.Goal.Id == thisGoal.Id)
                {
                    allTasks++;
                    if (item.Task.Accomplishment == true)
                        readyTasks++;
                }
                
            }
            return readyTasks + "/" + allTasks;
        }
        
        public int GetAllTasks(List<TaskOnGoal> taskOnGoals, Goal thisGoal )
        {
            int allTasks = 0;
            int readyTasks = 0;
            foreach (var item in taskOnGoals)
            {
                if (item.Goal.Id == thisGoal.Id)
                {
                    allTasks++;
                    if (item.Task.Accomplishment == true)
                        readyTasks++;
                }
                
            }
            return allTasks;
        }
        public int GetReadyTasks(List<TaskOnGoal> taskOnGoals, Goal thisGoal )
        {
            int allTasks = 0;
            int readyTasks = 0;
            foreach (var item in taskOnGoals)
            {
                if (item.Goal.Id == thisGoal.Id)
                {
                    allTasks++;
                    if (item.Task.Accomplishment == true)
                        readyTasks++;
                }
                
            }
            return readyTasks;
        }
        
        private void FolderListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void GoMainBtn_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(_user));
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AddGoalWindow addFolderWindow = new AddGoalWindow(_user);
            addFolderWindow.ShowDialog();
            this.IsEnabled = false;
            addFolderWindow.Close();
            this.IsEnabled = true;
            Lode();
        }

        private void ChangeButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void GoalPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Lode();
        }
    }
}