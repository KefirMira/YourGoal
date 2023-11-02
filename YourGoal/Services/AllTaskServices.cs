using System;
using System.Collections.Generic;
using Windows.ApplicationModel.UserDataAccounts.Provider;
using Npgsql;
using YourGoal.Models;

namespace YourGoal.Services
{
    public class AllTaskServices
    {
        private string connectionString = "Host = localhost; Database = yourgoaldb; User ID = postgres; Password= 2347";
        private static NpgsqlConnection _connection;
        public AllTaskServices()
        {
            _connection = new NpgsqlConnection(connectionString);
        }
        //таски на одного пользователя
        public static List<Task> GetAllTask(User user)
        {
            List<Task> tasks = new List<Task>();
            NpgsqlCommand command = new NpgsqlCommand($"select * from task where userid={user.Id}", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Task task = new Task();
                task.Id = Convert.ToInt32(reader["id"].ToString());
                task.Name = reader["name"].ToString();
                task.DateDelete = Convert.ToDateTime(reader["datedelete"].ToString());
                task.Folder = GetFolderForGoal(Convert.ToInt32(reader["folderId"].ToString()));
                task.Priority = GetPriorityToTask(Convert.ToInt32(reader["priorityid"].ToString()));
                task.User = GetUserToTask(Convert.ToInt32(reader["userid"].ToString()));
            }
            return tasks;
        }
        
        
        public static List<Task> GetAllTask()
        {
            List<Task> tasks = new List<Task>();
            NpgsqlCommand command = new NpgsqlCommand($"select * from task", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Task task = new Task();
                task.Id = Convert.ToInt32(reader["id"].ToString());
                task.Name = reader["name"].ToString();
                task.DateDelete = Convert.ToDateTime(reader["datedelete"].ToString());
                task.Folder = GetFolderForGoal(Convert.ToInt32(reader["folderId"].ToString()));
                task.Priority = GetPriorityToTask(Convert.ToInt32(reader["priorityid"].ToString()));
                task.User = GetUserToTask(Convert.ToInt32(reader["userid"].ToString()));
            }
            return tasks;
        }

        public static User GetUserToTask(int id)
        {
            NpgsqlCommand command = new NpgsqlCommand($"select * from "+ " user_ " +" where  " +
                                                      $"id = {id}", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            User taskUser = new User();
            while (reader.Read())
            {
                taskUser.Id = Convert.ToInt32(reader["id"]);
                taskUser.Name = reader["name"].ToString();
                taskUser.Login = reader["login"].ToString();
                taskUser.Password = reader["password"].ToString();
            }
            return taskUser;
        }
        
        public static Priority GetPriorityToTask(int id)
        {
            Priority priority = new Priority();
            NpgsqlCommand command = new NpgsqlCommand($"select * from priority where id= {id}", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                priority.Id = Convert.ToInt32(reader["id"].ToString());
                priority.Name = reader["name"].ToString();
            }
            
            return priority;
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
    }
}