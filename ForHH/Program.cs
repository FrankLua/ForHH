using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using Aspose.Cells;
using CsvHelper;
using CsvHelper.Configuration;
using ForHH.Models;
using SkiaSharp;
using static ForHH.CSV.CSV;
using static ForHH.SqlLite.Sqlite;

namespace ForHH
{
    internal class Program
    {
        static private string folderNotFound = "Этой директории не существует";
        static private string empytyStr = "Вы не указали путь";
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            bool forWhile = true;
            var code_name = new List<Code_Name>();
            var code_category = new List<Code_Category>();
            var name_division = new List<Code_Division>();
            bool forwhiletwo = true;

            while (forWhile)
            {
                Console.WriteLine("Введите путь к 'тестовому' файлу");
                string pathRead = Console.ReadLine();
                if(pathRead != "")
                {
                    DirectoryInfo folder = new DirectoryInfo(pathRead);
                    if (folder.Exists)
                    {
                        string[] files = Directory.GetFiles(pathRead);

                        if (files.Contains(pathRead + "\\" + "Тестовые данные.CSV"))
                        {
                            Console.WriteLine("Отлично файл нашёл, укажите путь куда записать данные CSV");
                            string pathWrite = Console.ReadLine();
                            if (pathWrite != "")
                            {
                                DirectoryInfo folderWrite = new DirectoryInfo(pathWrite);
                                if (folderWrite.Exists)
                                {
                                    var list = Read(pathRead);

                                    code_name = Get_Code_Names(list);

                                    code_category = Get_Code_Category(list);

                                    name_division = Get_Name_Division(list);

                                    bool num1 = WriteСategoriesCSV(code_category, pathWrite);

                                    bool num2 = WriteProcesseseCSV(code_name, pathWrite);

                                    bool num3 = WriteDivisionCSV(name_division, pathWrite);

                                    if (num1 && num2 && num3)
                                    {
                                        forWhile = false;
                                    }

                                }
                                else
                                {
                                    Console.WriteLine(folderNotFound);
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine(empytyStr);
                                continue;
                            }





                        }
                        else
                        {
                            Console.WriteLine("Файл 'Тестовые данные.CSV' не найден.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine(empytyStr);
                        continue;

                    }
                }
                else
                {
                    Console.WriteLine(empytyStr);
                    continue;
                }
            }
            
            while (forwhiletwo) 
            {
                Console.WriteLine("Хотите записать данные виде базы данных? (y/n)");
                ConsoleKeyInfo key = Console.ReadKey();
                
                switch (key.Key)
                {
                    case ConsoleKey.Y:
                        {
                            Console.WriteLine();
                            Console.WriteLine("Укажите путь где создать БД");
                            string dbPath = Console.ReadLine();
                            if(dbPath != "")
                            {
                                DirectoryInfo folder = new DirectoryInfo(dbPath);
                                if (folder.Exists)
                                {
                                    bool dbComplite = CreateDb(code_name,code_category,name_division, dbPath);
                                    if(dbComplite)
                                    {
                                        forwhiletwo = false;
                                            continue;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                    
                                }
                                else
                                {
                                    Console.WriteLine(folderNotFound);
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine(empytyStr);
                                continue;
                            }
                             
                            
                        }
                    case ConsoleKey.N:
                        {
                            Console.WriteLine("Хорошо");
                            forwhiletwo = false;
                            continue;
                        }
                    
                       
                }               
            }            
            Console.ReadLine();
        }

 
    }
}