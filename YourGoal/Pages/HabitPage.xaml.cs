using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xceed.Document.NET;
using Xceed.Words.NET;
using YourGoal.Models;
using YourGoal.Services;
using YourGoal.Windows;

namespace YourGoal.Pages
{
    public partial class HabitPage : Page
    {
        private Habit _habit;
        private User _user = new User();
        private DateTime _dateTime = new DateTime();
        public HabitPage()
        {
            InitializeComponent();
        }
        public HabitPage(User user)
        {
            InitializeComponent();
            _user = user;
            _habit = new Habit();
        }

        public void Lode()
        {
            AllHabitAndTrackerService allHabit = new AllHabitAndTrackerService();
            AllFolderService allFolderService = new AllFolderService();
            _dateTime = DateTime.Now;
            List<Habit> habits = allHabit.GetAllHabitForUser(_user,_dateTime);
            List<Folder> folders = allFolderService.GetFolders();
            
            AllHabbitsListView.ItemsSource = habits;
            FolderListView.ItemsSource = folders;
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

        private void FolderListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Folder folder = FolderListView.SelectedItem as Folder;
            AllHabitAndTrackerService allHabit = new AllHabitAndTrackerService();
            
            List<Habit> habits = allHabit.GetAllHabitForUser(_user,_dateTime);
            AllHabbitsListView.ItemsSource = habits.Where(c => c.Folder.Id == folder.Id).ToList();
        }

        private void GoMainBtn_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(_user));
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AddHabitWindow addHabit = new AddHabitWindow(_user);
            addHabit.ShowDialog();
            this.IsEnabled = false;
            addHabit.Close();
            this.IsEnabled = true;
            Lode();
        }

        private void PrintButton_OnClick(object sender, RoutedEventArgs e)
        {
            AllHabitAndTrackerService allHabit = new AllHabitAndTrackerService();
            List<Habit> habits = allHabit.GetAllHabitForUser(_user,_dateTime);
            string path = @"..\..\Documents\"+ _user.Login+DateTime.Now.ToString("dd.MM.yyyy")+".docx";
            
            DocX document = DocX.Create(path);
            foreach (var item in habits)
            {
                Paragraph paragraph = document.InsertParagraph();
                paragraph.AppendLine($"Наименование - {item.Name}\n Категория - {item.Folder.Name}\n Выполнено ______")
                    .Font("Times New Roman")
                    .FontSize(12)
                    .Alignment = Alignment.center;
            }
            document.Save();
        }

        private void AllHabbitsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _habit = AllHabbitsListView.SelectedItem as Habit;
        }

        private void HabitPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Lode();
        }

        private void ToDiagram_OnClick(object sender, RoutedEventArgs e)
        {
            DiagramWindow addHabit = new DiagramWindow(_user);
            addHabit.ShowDialog();
            this.IsEnabled = false;
            addHabit.Close();
            this.IsEnabled = true;
            Lode();
        }

        

        private void CalendarA_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            AllHabitAndTrackerService allHabitAndTrackerService = new AllHabitAndTrackerService();
            DateTime dateTime = (DateTime)CalendarA.SelectedDate;
            List<Habit> habits = allHabitAndTrackerService.GetAllHabitForUser(_user,dateTime);
            AllHabbitsListView.ItemsSource = habits;
        }

        private void CheckAc_OnChecked(object sender, RoutedEventArgs e)
        {
            AllHabitAndTrackerService allHabitAndTrackerService = new AllHabitAndTrackerService();
            if(allHabitAndTrackerService.CreateTracker(_habit.Id,_dateTime,true));
        }
    }
}