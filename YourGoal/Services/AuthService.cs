using System;
using Npgsql;
using YourGoal.Models;

namespace YourGoal.Services
{
    public class AuthService
    {
        private string connectionString = "Host = localhost; Database = yourgoaldb; User ID = postgres; Password= 2347";
        private NpgsqlConnection _connection;
        public AuthService()
        {
            _connection = new NpgsqlConnection(connectionString);
        }
        public User AuthUser(string login, string password)
        {
            _connection.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select * from "+ " user_ " +" where  " +
                                                      $"password='{password}'  and login='{login}'", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            User authUser = new User();
            while (reader.Read())
            {
                authUser.Name = reader["name"].ToString();
                authUser.Surname = reader["surname"].ToString();
                authUser.BirthDay = Convert.ToDateTime(reader["birthday"].ToString());
                authUser.Login = reader["login"].ToString();
                authUser.Password = reader["password"].ToString();
                authUser.Id = Convert.ToInt32(reader["id"]);
            }
            return authUser;
        }
    }
}