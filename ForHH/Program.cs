using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Aspose.Cells;
using CsvHelper;
using CsvHelper.Configuration;
using ForHH.Models;
using SkiaSharp;
using static ForHH.CSV.CSV;

namespace ForHH
{
    internal class Program
    {
        static private string folderNotFound = "Этой директории не существует";
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            bool forWhile = true;
            while (true)
            {
                Console.WriteLine("Введите путь к 'тестовому' файлу");
                string pathRead = Console.ReadLine();
                DirectoryInfo folder = new DirectoryInfo(pathRead);
                if (folder.Exists)
                {
                    string[] files = Directory.GetFiles(pathRead);

                    if (files.Contains(pathRead +"\\"+ "Тестовые данные.CSV"))
                    {
                        Console.WriteLine("Отлично файл нашёл, укажите путь куда записать данные");
                        string pathWrite = Console.ReadLine();
                        DirectoryInfo folderWrite = new DirectoryInfo(pathWrite);
                        if (folderWrite.Exists)
                        {
                            var list = Read(pathRead);

                            var code_name = Get_Code_Names(list);

                            var code_category = Get_Code_Category(list);

                            var name_division = Get_Name_Division(list);

                            WriteСategoriesCSV(code_category, pathWrite);

                            WriteProcesseseCSV(code_name, pathWrite);

                            WriteDivisionCSV(name_division, pathWrite);

                        }
                        else
                        {
                            Console.WriteLine(folderNotFound);
                        }


                    }
                    else
                    {
                        Console.WriteLine("Файл 'Тестовые данные.CSV' не найден.");
                    }
                }
                else
                {
                    Console.WriteLine(folderNotFound);

                }
            }
            

            

           


        }

 
    }
}