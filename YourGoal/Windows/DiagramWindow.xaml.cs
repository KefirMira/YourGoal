using System;
using System.Collections.Generic;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using YourGoal.Models;
using YourGoal.Models.ProgrammModels;
using YourGoal.Services;

namespace YourGoal.Windows
{
    
    public partial class DiagramWindow : Window
    {
        Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        private User _user;
        public DiagramWindow()
        {
            InitializeComponent();
        }
        public DiagramWindow(User user)
        {
            InitializeComponent();
            _user = user;
            AllHabitAndTrackerService allHabitAndTrackerService = new AllHabitAndTrackerService();
            List<Habit> habits = new List<Habit>();
            habits = allHabitAndTrackerService.GetAllHabitForUser(_user,DateTime.Now);
            AllFolderService allFolderService = new AllFolderService();
            List<Folder> folders = allFolderService.GetFolders();
            List<ClassForHabit> forHabits = new List<ClassForHabit>();
            foreach (var item in folders)
            {
                ClassForHabit classForHabit = new ClassForHabit();
                classForHabit.NameHabit = item.Name;
                int count = 0;
                foreach (var it in habits)
                {
                    if (it.Folder.Name == item.Name)
                        count++;
                }

                classForHabit.Count = count;
                forHabits.Add(classForHabit);
            }

            SeriesCollection ser = new SeriesCollection();
            foreach (var item in forHabits)
            {
                PieSeries pie = new PieSeries
                {
                    Title = item.NameHabit,
                    Values = new ChartValues<double> { item.Count },
                    DataLabels = true,
                    LabelPoint = labelPoint,

                };
                ser.Add(pie);
            }

            pipChart.Series = ser;
        }
    }
}