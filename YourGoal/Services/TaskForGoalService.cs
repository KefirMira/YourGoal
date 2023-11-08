using System;
using System.Collections.Generic;
using Npgsql;
using YourGoal.Models;

namespace YourGoal.Services
{
    public class TaskForGoalService
    {
        private string connectionString = "Host = localhost; Database = yourgoaldb; User ID = postgres; Password= 2347 ";
        private static NpgsqlConnection _connection;
        private static NpgsqlConnection _connection2;
        public TaskForGoalService()
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection2 = new NpgsqlConnection(connectionString);
        }
        //таски на одного пользователя
        public List<Task> GetAllTask(User user)
        {
            //ААААААААААААБЛЯТЬЗАЛУПА
            //NpgsqlCommand command = new NpgsqlCommand($"select *  from task t join folder f on t.folderId = f.id join priority p on p.id = t.priorityId where userId = {user.Id}", _connection);
            //NpgsqlCommand command = new NpgsqlCommand($"select * from task where userid={user.Id}",_connection);
            // NpgsqlDataReader reader = command.ExecuteReader();
            // while (reader.Read())
            // {
            //     Task task = new Task();
            //     task.Id = Convert.ToInt32(reader.GetInt32(reader.GetOrdinal("t_id")).ToString());
            //     task.Name = reader.GetString(reader.GetOrdinal("t_name")).ToString();
            //     task.DateDelete = reader.GetDateTime(reader.GetOrdinal("t_deleteDate"));
            //     Folder folder = new Folder();
            //     folder.Name = reader.GetString(reader.GetOrdinal("f_name"));
            //     folder.Id =   reader.GetInt32(reader.GetOrdinal("f_id"));
            //     Priority priority = new Priority();
            //     priority.Name = reader.GetString(reader.GetOrdinal("p_name"));
            //     priority.Id =   reader.GetInt32(reader.GetOrdinal("p_id"));
            //     task.Folder = folder;
            //     task.Priority = priority;
            //     task.User = user;
            //     tasks.Add(task);
            // }
            // return tasks;
            List<Task> rr = new List<Task>();
            return rr;
        }
        
        
        
        
        public List<TaskOnGoal> GetAllTaskForGoal()
        {
            _connection.Open();
            List<TaskOnGoal> tasks = new List<TaskOnGoal>();
            NpgsqlCommand command = new NpgsqlCommand($"select * from taskForGoal", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TaskOnGoal task = new TaskOnGoal();
                task.Id = reader.GetInt32(reader.GetOrdinal("id"));
                //task.IdTask = reader.GetInt32(reader.GetOrdinal("goalid"));
                //task.IdGoal = reader.GetInt32(reader.GetOrdinal("taskid"));
                tasks.Add(task);
            }

            return tasks;
        }
        public List<TaskOnGoal> GetAllTaskAndGoal(User user)
        {
            _connection.Open();
            List<TaskOnGoal> tasks = new List<TaskOnGoal>();
            //полный запрос
            //NpgsqlCommand command = new NpgsqlCommand($"select t.id t_id, t.name t_name, t.dateDelete t_dateDelete, g.name g_name, g.id g_id, g.dateEnd g_dateEnd, g.dateStart d_dateStart, gf.id gf_id, gf.name gf_name, tf.id tf_id, tf.name tf_name, p.id p_id, p.name p_name from taskForGoal    join task t on taskForGoal.taskId = t.id    join goal g on g.id = taskForGoal.goalId    join folder gf on gf.id = g.folderId    join folder tf on tf.id = t.folderId    join priority p on t.priorityId = p.id where t.userId =1", _connection);
            //только таски и гоалы
            NpgsqlCommand command = new NpgsqlCommand($"select  tfg.id tfg_id,tfg.goalId tfg_golaId, tfg.taskId tfg_taskId,t.id t_id, t.name t_name, t.dateDelete t_dateDelete, t.folderId t_folderId, t.priorityId t_priorityId, g.name g_name, g.id g_id, g.dateEnd g_dateEnd, g.dateStart g_dateStart, g.folderId g_folderId from taskForGoal tfg    join task t on tfg.taskId = t.id    join goal g on g.id = tfg.goalId where t.userId ={user.Id}", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TaskOnGoal taskGoal = new TaskOnGoal();
                taskGoal.Id = reader.GetInt32(reader.GetOrdinal("tfg_id"));
                Task task = new Task();
                task.Id = reader.GetInt32(reader.GetOrdinal("t_id"));
                task.User = user;
                task.DateDelete = reader.GetDateTime(reader.GetOrdinal("t_dateDelete"));
                task.Name = reader.GetString(reader.GetOrdinal("t_name"));
                Goal goal = new Goal();

                try
                {
                    //если нет папок, то скип
                    Folder t_folder = new Folder();
                    t_folder = GetFolderForTask(reader.GetInt32(reader.GetOrdinal("t_folderId")));
                    Priority t_priority = GetPriority(reader.GetInt32(reader.GetOrdinal("t_priorityId")));
                    task.Folder = t_folder;
                    task.Priority = t_priority;
                    Folder g_folder = GetFolderForTask(reader.GetInt32(reader.GetOrdinal("t_folderId")));
                    goal.Folder = g_folder;
                }
                catch
                {
                    
                }
                // t_folder.Id = reader.GetInt32(reader.GetOrdinal("tf_id"));
                // t_folder.Name = reader.GetString(reader.GetOrdinal("tf_name"));
                // t_priority.Id = reader.GetInt32(reader.GetOrdinal("p_id"));
                // t_priority.Name = reader.GetString(reader.GetOrdinal("p_name"));
                goal.Id = reader.GetInt32(reader.GetOrdinal("g_id"));
                goal.Name = reader.GetString(reader.GetOrdinal("g_name"));
                goal.DateStart = reader.GetDateTime(reader.GetOrdinal("g_dateStart"));
                goal.DateEnd = reader.GetDateTime(reader.GetOrdinal("g_dateEnd"));
                // g_folder.Id = reader.GetInt32(reader.GetOrdinal("gf_id"));
                // g_folder.Name = reader.GetString(reader.GetOrdinal("gf_name"));
                taskGoal.Task = task;
                taskGoal.Goal = goal;
                tasks.Add(taskGoal);
            }

            return tasks;
        }
        
        public  Folder GetFolderForTask(int Id)
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

            return folderForGoal;
        }
        
        public  Priority GetPriority(int Id)
        {
            _connection2.Open();
            Priority priority = new Priority();
            NpgsqlCommand command = new NpgsqlCommand($"select * from priority where id={Id}", _connection2);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                priority.Id = Convert.ToInt32(reader["id"]);
                priority.Name = reader["name"].ToString();
            }

            return priority;
        }
    }
}