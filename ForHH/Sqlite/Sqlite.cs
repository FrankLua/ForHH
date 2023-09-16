using Aspose.Cells.Revisions;
using ForHH.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ForHH.SqlLite
{
    public class Sqlite
    {
        public static bool CreateDb(List<Code_Name>inserListName, List<Code_Category> inserListCategory, List<Code_Division> inserListDivision,string path)
        {
            var fullstringpath = path + "\\" + "DbAnswer.db";
            try
            {
                FileInfo fileInf = new FileInfo(path);
                string[] files = Directory.GetFiles(path);
                if (files.Contains(fullstringpath))
                {
                    FileInfo db = new FileInfo(fullstringpath);
                    db.Delete();                  
                    
                    
                }
                using (File.Create(fullstringpath)) { };
                Console.WriteLine("БД создана");
                using (var connection = new SqliteConnection($"Data Source={fullstringpath}"))
                {
                    connection.Open();
                    SetCode_Name(connection, inserListName);
                    SetCode_Category(connection, inserListCategory);
                    SetCode_Division(connection, inserListDivision);
                    connection.Close();
                    Console.WriteLine("Таблицы добавленны");

                }

                return true;
            }
            catch(System.IO.IOException ex)
            {
                Console.WriteLine("Закройте файл!");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка");
                return false;
            }

        }
        public static bool SetCode_Name(SqliteConnection connection, List<Code_Name> inserList)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "CREATE TABLE Processes (\r\n    [Код процесса]          TEXT PRIMARY KEY,\r\n    [Наименование процесса] TEXT\r\n);";
            
            command.ExecuteNonQuery();
            command.CommandText = "Insert into Processes Values";
            foreach (var inser in inserList)
            {
                command.CommandText += $"('{inser.Code}','{inser.Name_Process}'),";
            }
            command.CommandText = command.CommandText.TrimEnd(',');
            command.ExecuteNonQuery();     
            


            Console.WriteLine("Таблица Processes Создана");
            return true;

        }
        public static bool SetCode_Division(SqliteConnection connection, List<Code_Division> inserList)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "CREATE TABLE Divisions (\r\n    Id                                INTEGER PRIMARY KEY AUTOINCREMENT,\r\n    [Подразделение-владелец процесса] TEXT,\r\n    [Код процесса]                    TEXT\r\n);";

            command.ExecuteNonQuery();
            command.CommandText = "Insert into Divisions Values";
            foreach (var inser in inserList)
            {
                if (inser.Division != "")
                {
                    command.CommandText += $"(null,'{inser.Division}','{inser.Code}'),";
                }
                    
            }
            command.CommandText = command.CommandText.TrimEnd(',');
            command.ExecuteNonQuery();



            Console.WriteLine("Таблица Division Создана");
            return true;

        }
        public static bool SetCode_Category(SqliteConnection connection, List<Code_Category> inserList)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "CREATE TABLE Categories (\r\n    [Код процесса]       TEXT PRIMARY KEY,\r\n    [Категория процесса] TEXT\r\n);";

            command.ExecuteNonQuery();
            command.CommandText = "Insert into Categories Values";
            foreach (var inser in inserList)
            {
                command.CommandText += $"('{inser.Code}','{inser.Category}'),";
            }
            command.CommandText = command.CommandText.TrimEnd(',');
            command.ExecuteNonQuery();

            Console.WriteLine("Таблица Categories Создана");
            return true;

        }

    }
}
