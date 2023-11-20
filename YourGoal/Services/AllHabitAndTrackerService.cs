using System;
using System.Collections.Generic;
using Npgsql;
using YourGoal.Models;

namespace YourGoal.Services
{
    public class AllHabitAndTrackerService
    {
        private string connectionString = "Host = localhost; Database = yourgoaldb; User ID = postgres; Password= 2347";
        private static NpgsqlConnection _connection;
        private static NpgsqlConnection _connection2;
        public AllHabitAndTrackerService()
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection2 = new NpgsqlConnection(connectionString);
        }
        //все папки
        public  List<Habit> GetAllHabitForUser(User user)
        {
            _connection.Open();
            List<Habit> habits = new List<Habit>();
            NpgsqlCommand command = new NpgsqlCommand($"select * from habit where userid={user.Id}", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Habit habit = new Habit();
                habit.Id = Convert.ToInt32(reader["id"]);
                habit.Name = reader["name"].ToString();
                habit.DateEnd = Convert.ToDateTime(reader["dateend"]);
                habit.DateStart = Convert.ToDateTime(reader["datestart"]);
                habit.User = user;
                habit.Folder = GetFolderForHabit(Convert.ToInt32(reader["folderid"]));
                habit.Trackers = GetTracker(Convert.ToInt32(reader["id"]));
                habits.Add(habit);
            }

            return habits;
        }

        public static List<Tracker> GetTracker(int Id)
        {
            _connection2.Open();
            List<Tracker> trackers = new List<Tracker>();
            NpgsqlCommand command = new NpgsqlCommand($"select * from tracker where habitid={Id}", _connection2);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Tracker tracker = new Tracker();
                tracker.Id = Convert.ToInt32(reader["id"]);
                tracker.HabitId = Convert.ToInt32(reader["habitid"]);
                tracker.DateOfCompletion = Convert.ToDateTime(reader["dateofcompletion"]);
                tracker.Accomplishment = Convert.ToBoolean(reader["accomplishment"]);
                trackers.Add(tracker);
            }
            _connection2.Close();
            return trackers;
        }
        
        public static Folder GetFolderForHabit(int Id)
        {
            _connection2.Open();
            Folder folderForGoal = new Folder();
            NpgsqlCommand command = new NpgsqlCommand($"select * from folder where id={Id}", _connection2);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                folderForGoal.Id = Convert.ToInt32(reader["id"]);
                folderForGoal.Name = reader["name"].ToString();
            }
            _connection2.Close();
            return folderForGoal;
        }
        
        //создание новой
        public static bool CreateNewHabit(string nameFolder)
        {
            //не готово   
            NpgsqlCommand command = new NpgsqlCommand($"insert into folder(name) values ('{nameFolder}');", _connection);
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}