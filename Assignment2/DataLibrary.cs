using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Assignment2
{
    public class DataRepo
    {
        public static string DBName = ApplicationData.Current.LocalFolder.Path + @"\OneNoteProg.db";
        public static string ConnectionName = "Filename=" + DBName;

        //Initalize the Database
        public static void InitializeDB()
        {

            if (!File.Exists(DBName))
            {
                SQLiteConnection.CreateFile(DBName);
            }

            SqliteConnection db = new SqliteConnection(ConnectionName);
            using (db)
            {
                db.Open();
                String createTable = "CREATE TABLE IF NOT EXISTS " +
                    "NoteTable (NoteID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "NoteName nvarchar(100) NOT NULL, NoteContent nvarchar(800) NOT NULL);";

                SqliteCommand create = new SqliteCommand(createTable, db);
                create.ExecuteReader();
            }

        }

        //Add Data
        public static void AddData(string inputContent, string inputName)
        {
            using (SqliteConnection db =
                new SqliteConnection(ConnectionName))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO NoteTable " +
                    "VALUES (NULL, @entry, @entry2);";

                insertCommand.Parameters.AddWithValue("@entry", inputName);
                insertCommand.Parameters.AddWithValue("@entry2", inputContent);

                insertCommand.ExecuteReader();

            }
        }

        //Update Existing Records
        public static void UpdateData(int ID, string text)
        {
            using (SqliteConnection db =
                new SqliteConnection(ConnectionName))
            {
                db.Open();

                SqliteCommand updateCommand = new SqliteCommand();
                updateCommand.Connection = db;

                updateCommand.CommandText = "UPDATE NoteTable set NoteContent = @entry where NoteID = " + ID + ";";

                updateCommand.Parameters.AddWithValue("@entry", text);

                updateCommand.ExecuteReader();

            }
        }

        public static void DeleteData(int ID)
        {
            using (SqliteConnection db =
                new SqliteConnection(ConnectionName))
            {
                db.Open();

                SqliteCommand deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;

                deleteCommand.CommandText = "DELETE FROM NoteTable WHERE NoteID='" + ID + "';";

                deleteCommand.ExecuteReader();

            }
        }

        //Get the data in a List
        public static List<TextFileModel> GetData()
        {
            List<TextFileModel> entries = new List<TextFileModel>();

            using (SqliteConnection db =
               new SqliteConnection(ConnectionName))
            {

                db.Open();

                SqliteCommand selectCommand =
                    new SqliteCommand("SELECT NoteName, NoteContent, NoteID FROM NoteTable;", db);

                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    entries.Add(GetTextFileModel(query));
                }


                return entries;
            }
        }


        //Create the TextFile Objects
        private static TextFileModel GetTextFileModel(IDataRecord record)
        {
            return new TextFileModel(record.GetInt32(2), record.GetString(0), record.GetString(1));
        }
    }
}
