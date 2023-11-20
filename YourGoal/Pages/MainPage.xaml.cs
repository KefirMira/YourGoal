using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using YourGoal.Models;
using YourGoal.Models.ProgrammModels;
using YourGoal.Services;

namespace YourGoal.Pages
{
    public partial class MainPage : Page
    {
        private User _user;
        public MainPage()
        {
            InitializeComponent();
        }
        public MainPage(User user)
        {
            InitializeComponent();
            _user = user;
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
        
        public void WorkWithDiagramm(List<TaskOnGoal> taskOnGoals, Goal thisGoal )
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
            GaugeForGoal.To = allTasks;
            GaugeForGoal.Value = readyTasks;
        }

        private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<TaskOnGoal> taskOnGoals = new List<TaskOnGoal>();
                TaskForGoalService taskForGoalService = new TaskForGoalService();
                taskOnGoals = taskForGoalService.GetAllTaskAndGoal(_user);
                List<Task> task = new List<Task>();
                List<Goal> goal = new List<Goal>();
                foreach (var item in taskOnGoals)
                {
                    Goal thgoal = new Goal();
                    thgoal = item.Goal;
                    thgoal.RelationshipTasks = RelationshipTasksCounter(taskOnGoals, item.Goal);
                    int count = thgoal.DateStart.Day - thgoal.DateEnd.Day;
                    thgoal.RemainingDays = count;
                    task.Add(item.Task);
                    goal.Add(thgoal);
                }
                AllTaskServices allTaskServices = new AllTaskServices();
                task = allTaskServices.GetAllTask(_user); 
                AllTaskListView.ItemsSource = task;
                //goal[0].RelationshipTasks = RelationshipTasksCounter(taskOnGoals, goal[0]);
                GoalPanel.DataContext = goal[0];
                WorkWithDiagramm(taskOnGoals, goal[0]);
                AllHabitAndTrackerService habitService = new AllHabitAndTrackerService();
                List<Habit> habits = habitService.GetAllHabitForUser(_user);
                HabitsListView.ItemsSource = habits;
                CalendarListView.ItemsSource = GetActualCalendar();
            }
            catch
            {
                MessageBox.Show("Ошибка считывания данных!");
            }
        }

        public string GetDateName(string fullName)
        {
            if (fullName == "Monday")
                return "ПН";
            else if(fullName=="Tuesday")
                return "ВТ";
            else if(fullName=="Wednesday")
                return "СР";
            else if(fullName=="Thursday")
                return "ЧТ";
            else if(fullName=="Friday")
                return "ПТ";
            else if(fullName=="Saturday")
                return "СБ";
            else 
                return "ВС";
        }
        
        
        public List<CalendarMod> GetActualCalendar()
        {
            List<CalendarMod> calendar = new List<CalendarMod>();
            for (int i = 0; i < 12; i++)
            {
                CalendarMod cal = new CalendarMod();
                if (i == 0)
                {
                    DateTime now = DateTime.Today.AddDays(-1);
                    cal.DateNum = now.Day;
                    cal.DateName = GetDateName(now.DayOfWeek.ToString());
                }
                else if (i == 1)
                {
                    DateTime now = DateTime.Today;
                    cal.DateNum = now.Day;
                    cal.DateName = GetDateName(now.DayOfWeek.ToString());
                }
                else
                {
                    DateTime now = DateTime.Today.AddDays(i);
                    cal.DateNum = now.Day;
                    cal.DateName = GetDateName(now.DayOfWeek.ToString());
                }
                calendar.Add(cal);
            }
            return calendar;
        }

        private void TasksCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            AllTaskServices allTaskServices = new AllTaskServices();
            Task task = AllTaskListView.SelectedItem as Task;
            allTaskServices.ChangeStatusTask(task);
        }

        private void HabitCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void HoldButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigateStackPanel.Visibility = Visibility.Collapsed;
        }

        private void MainPageTransitionButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(_user));
        }

        private void TasksPageTransitionButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksPage(_user));
        }

        private void GoalPageTransitionButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GoalPage(_user));
        }

        private void HabitPageTransitionButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HabitPage(_user));
        }
        

        private void NavigateButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigateStackPanel.Visibility = Visibility.Visible;
        }

        private void ToGoalButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GoalPage(_user));
        }

        private void ToTasksButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksPage(_user));
        }

        private void ToHabitButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HabitPage(_user));
        }

        private void ChangeButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GoalPage(_user));
        }
    }
}