using System;
using System.Collections.Generic;
using Npgsql;
using YourGoal.Models;

namespace YourGoal.Services
{
    public class AllPriorityService
    {
        private string connectionString = "Host = localhost; Database = yourgoaldb; User ID = postgres; Password= biba";
        private static NpgsqlConnection _connection;
        public AllPriorityService()
        {
            _connection = new NpgsqlConnection(connectionString);
        }
        //все приоритеты
        public static List<Priority> GetFolders()
        {
            List<Priority> priorities = new List<Priority>();
            NpgsqlCommand command = new NpgsqlCommand($"select * from priority", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Priority priority = new Priority();
                priority.Id = Convert.ToInt32(reader["id"]);
                priority.Name = reader["name"].ToString();
                priorities.Add(priority);
            }

            return priorities;
        }
    }
}