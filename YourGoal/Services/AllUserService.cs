using System;
using System.Threading.Tasks;
using Npgsql;
using YourGoal.Models;

namespace YourGoal.Services
{
    public class AllUserService
    {
        private string connectionString = "Host = localhost; Database = yourgoaldb; User ID = postgres; Password= 2347";
        private NpgsqlConnection _connection;
        private NpgsqlConnection _connection2;
        public AllUserService()
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection2 = new NpgsqlConnection(connectionString);
        }
        //авторизация
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
                // authUser.Surname = reader["surname"].ToString();
                // authUser.BirthDay = Convert.ToDateTime(reader["birthday"].ToString());
                authUser.Login = reader["login"].ToString();
                authUser.Password = reader["password"].ToString();
                authUser.Id = Convert.ToInt32(reader["id"]);
            }
            return authUser;
        }
        //регистрация
        public  bool CreateNewUser(string Name, string Login, string Password)
        {
            _connection.Open();
            NpgsqlCommand searchUser = new NpgsqlCommand($"select * from "+ " user_ " +" where  " +
                                                      $" login='{Login}'", _connection);
            NpgsqlDataReader reader = searchUser.ExecuteReader();
            User authUser = new User();
            while (reader.Read())
            {
                authUser.Name = reader["name"].ToString();
                // authUser.Surname = reader["surname"].ToString();
                // authUser.BirthDay = Convert.ToDateTime(reader["birthday"].ToString());
                authUser.Login = reader["login"].ToString();
                authUser.Password = reader["password"].ToString();
                authUser.Id = Convert.ToInt32(reader["id"]);
            }
            if (authUser.Id != 0)
                return false;
            else
            {
                _connection2.Open();
                NpgsqlCommand command = new NpgsqlCommand($"insert into user_(name,login,password) values ('{Name}','{Login}','{Password}');", _connection2);
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
        //смена пароля
        public  bool RecoveryPassword(string Login,string Password)
        {
            
            //int id =  FindUser(Login);
            _connection.Open();
            NpgsqlCommand command = new NpgsqlCommand($"UPDATE user_ SET password = '{Password}' where login='{Login}'", _connection);
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
        
        
        
        //поиск пользователя по логину
        public  int FindUser(string Login)
        {
            _connection2.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select * from user_ where login='{Login}'", _connection);
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
                User authUser = new User();
                while (reader.Read())
                {
                    authUser.Name = reader["name"].ToString();
                    authUser.Login = reader["login"].ToString();
                    authUser.Password = reader["password"].ToString();
                    authUser.Id = Convert.ToInt32(reader["id"]);
                }
                _connection2.Close();
                return authUser.Id;
            }
            catch
            {
                return 0;
            }  
            
        }
    }
}