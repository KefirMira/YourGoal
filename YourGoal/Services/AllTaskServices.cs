using System;
using System.Collections.Generic;
using Windows.ApplicationModel.UserDataAccounts.Provider;
using Npgsql;
using YourGoal.Models;

namespace YourGoal.Services
{
    public class AllTaskServices
    {
        private string connectionString = "Host = localhost; Database = yourgoaldb; User ID = postgres; Password= 2347 ";
        private static NpgsqlConnection _connection;
        public AllTaskServices()
        {
            _connection = new NpgsqlConnection(connectionString);
        }
        //таски на одного пользователя
        public List<Task> GetAllTask(User user)
        {
            _connection.Open();
            NpgsqlCommand command1 = new NpgsqlCommand("set client_encoding = 'UTF8'", _connection);
            command1.ExecuteNonQuery();
            List<Task> tasks = new List<Task>();
            NpgsqlCommand command = new NpgsqlCommand($"select t.id t_id, t.name t_name, t.dateDelete t_deleteDate, p.name p_name,p.id p_id, f.id f_id, f.name f_name,t.accomplishment t_accomplishment  from task t join folder f on t.folderId = f.id join priority p on p.id = t.priorityId where userId = {user.Id}", _connection);
            //NpgsqlCommand command = new NpgsqlCommand($"select *  from task t join folder f on t.folderId = f.id join priority p on p.id = t.priorityId where userId = {user.Id}", _connection);
            //NpgsqlCommand command = new NpgsqlCommand($"select * from task where userid={user.Id}",_connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Task task = new Task();
                task.Id = Convert.ToInt32(reader.GetInt32(reader.GetOrdinal("t_id")).ToString());
                task.Name = reader.GetString(reader.GetOrdinal("t_name")).ToString();
                task.DateDelete = reader.GetDateTime(reader.GetOrdinal("t_deleteDate"));
                var accompl = reader.GetBoolean(reader.GetOrdinal("t_accomplishment"));
                task.Accomplishment = accompl;
                Folder folder = new Folder();
                folder.Name = reader.GetString(reader.GetOrdinal("f_name"));
                folder.Id =   reader.GetInt32(reader.GetOrdinal("f_id"));
                Priority priority = new Priority();
                priority.Name = reader.GetString(reader.GetOrdinal("p_name"));
                priority.Id =   reader.GetInt32(reader.GetOrdinal("p_id"));
                task.Folder = folder;
                task.Priority = priority;
                task.User = user;
                tasks.Add(task);
            }
            return tasks;
        }
        
        
        public List<Task> GetAllTask()
        {
            List<Task> tasks = new List<Task>();
            NpgsqlCommand command = new NpgsqlCommand($"select * from task join folder f on task.folderId = f.id join priority p on p.id = task.priorityId ", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Task task = new Task();
                task.Id = Convert.ToInt32(reader["id"].ToString());
                task.Name = reader["name"].ToString();
                task.DateDelete = Convert.ToDateTime(reader["datedelete"].ToString());
                Folder folder = new Folder();
                folder.Name = reader["f.name"].ToString();
                folder.Id = Convert.ToInt32(reader["f.id"].ToString());
                Priority priority = new Priority();
                priority.Name = reader["p.name"].ToString();
                priority.Id = Convert.ToInt32(reader["p.id"].ToString());
                task.Folder = folder;
                task.Priority = priority;
                task.User = GetUserToTask(Convert.ToInt32(reader["userid"].ToString()));
            }
            return tasks;
        }

        public User GetUserToTask(int id)
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
        
        public Priority GetPriorityToTask(int id)
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
        
        public  Folder GetFolderForTask(int Id)
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
        //обновление статуса задачи
        public bool ChangeStatusTask(Task task)
        {
            _connection.Open();
            NpgsqlCommand command = new NpgsqlCommand($"update task set accomplishment = {task.Accomplishment} where task.id = {task.Id} ", _connection);
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
        
        //обновление задачи
        public bool ChangeTask(Task task)
        {
            _connection.Open();
            string dateDelete = task.DateDelete.ToString("dd.MM.yyyy");
            NpgsqlCommand command = new NpgsqlCommand($"update task set folderId={task.Folder.Id}, priorityId={task.Priority.Id},name='{task.Name}',dateDelete='{dateDelete}' where id={task.Id} ", _connection);
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
        
        //Создание задачи
        public bool AddNewTask(Task task)
        {
            _connection.Open();
            string dateDelete = task.DateDelete.ToString("dd.MM.yyyy");
            NpgsqlCommand command = new NpgsqlCommand($"insert into task(name, dateDelete, priorityId, folderId, userId,accomplishment) values ('{task.Name}','{dateDelete}','{task.Priority.Id}','{task.Folder.Id}',{task.User.Id},'0')", _connection);
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