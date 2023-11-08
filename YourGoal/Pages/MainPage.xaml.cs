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

        private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            List<TaskOnGoal> taskOnGoals = new List<TaskOnGoal>();
            TaskForGoalService taskForGoalService = new TaskForGoalService();
            taskOnGoals = taskForGoalService.GetAllTaskAndGoal(_user);
            List<Task> task = new List<Task>();
            List<Goal> goal = new List<Goal>();
            foreach (var item in taskOnGoals)
            {
                task.Add(item.Task);
                goal.Add(item.Goal);
            }
            
            AllTaskListView.ItemsSource = task.Distinct();
            GoalPanel.DataContext = goal[0];
            AllHabitAndTrackerService habitService = new AllHabitAndTrackerService();
            List<Habit> habits = habitService.GetAllHabitForUser(_user);
            HabitsListView.ItemsSource = habits;
            CalendarListView.ItemsSource = GetActualCalendar();
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
    }
}