using System;
using System.Collections.Generic;
using Npgsql;
using YourGoal.Models;

namespace YourGoal.Services
{
    public class AllGoalServices
    {
        private string connectionString = "Host = localhost; Database = yourgoaldb; User ID = postgres; Password= biba";
        private static NpgsqlConnection _connection;
        public AllGoalServices()
        {
            _connection = new NpgsqlConnection(connectionString);
        }
        // Выгружаем все цели
        #region GetGoal
        public  List<Goal> LoadAllGoal()
        {
            _connection.Open();
            List<Goal> goals = new List<Goal>();
            NpgsqlCommand command = new NpgsqlCommand($"select * from goal", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Goal goal = new Goal();
                goal.Id = Convert.ToInt32(reader["id"].ToString());
                goal.Name = reader["name"].ToString();
                goal.DateEnd = Convert.ToDateTime(reader["surname"].ToString());
                goal.DateStart = Convert.ToDateTime(reader["surname"].ToString());
                goal.Folder = GetFolderForGoal(Convert.ToInt32(reader["folderId"].ToString()));
                //goal.Tasks = GetAllTaskToGoal(Convert.ToInt32(reader["id"].ToString()));
            }
            return goals;
        }

        public static List<Task> GetAllTaskToGoal(int id)
        {
            List<Task> allTasks = new List<Task>();
            NpgsqlCommand command = new NpgsqlCommand($"select * from taskforgoal where goalid= {id}", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Task task = new Task();
                task.Id = Convert.ToInt32(reader["id"]);
                task.Name = reader["name"].ToString();
                allTasks.Add(task);
            }
            
            return allTasks;
        }
        
        
        public static User GetUserForGoal(int id)
        {
            User userForGoal = new User();
            NpgsqlCommand command = new NpgsqlCommand($"select * from user_ where id = {id}", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                userForGoal.Name = reader["name"].ToString();
                userForGoal.Surname = reader["surname"].ToString();
                userForGoal.BirthDay = Convert.ToDateTime(reader["birthday"].ToString());
                userForGoal.Login = reader["login"].ToString();
                userForGoal.Password = reader["password"].ToString();
                userForGoal.Id = Convert.ToInt32(reader["id"]);
            }
            
            return userForGoal;
        }

        public static Folder GetFolderForGoal(int Id)
        {
            Folder folderForGoal = new Folder();
            NpgsqlCommand command = new NpgsqlCommand($"select * from folder where id={Id}", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                folderForGoal.Id = Convert.ToInt32(reader["id"]);
                folderForGoal.Name = reader["name"].ToString();
            }

            return folderForGoal;
        }
        #endregion
        //Создание цели
        public bool AddNewGoal(Goal goal)
        {
            _connection.Open();
            string dateStart = goal.DateStart.ToString("dd.MM.yyyy");
            string dateEnd = goal.DateEnd.ToString("dd.MM.yyyy");
            NpgsqlCommand command = new NpgsqlCommand($"insert into goal(name, dateStart, dateEnd, folderId) values ('{goal.Name}','{dateStart}','{dateEnd}',{goal.Folder.Id})", _connection);
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