using System;
using System.Collections.Generic;
using Npgsql;
using YourGoal.Models;

namespace YourGoal.Services
{
    public class AllFolderService
    {
        private string connectionString = "Host = localhost; Database = yourgoaldb; User ID = postgres; Password= biba";
        private static NpgsqlConnection _connection;
        public AllFolderService()
        {
            _connection = new NpgsqlConnection(connectionString);
        }
        //все папки
        public  List<Folder> GetFolders()
        {
            _connection.Open();
            List<Folder> folders = new List<Folder>();
            NpgsqlCommand command = new NpgsqlCommand($"select * from folder", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Folder folder = new Folder();
                folder.Id = Convert.ToInt32(reader["id"]);
                folder.Name = reader["name"].ToString();
                folders.Add(folder);
            }

            return folders;
        }
        //создание новой
        public  bool CreateNewFolder(string nameFolder)
        {
            _connection.Open();
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