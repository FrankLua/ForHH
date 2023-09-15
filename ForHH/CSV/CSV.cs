using ForHH.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ForHH.CSV
{
    public class CSV
    {
        private static string fileName = "Тестовые данные.CSV";
        public static List<List<string>> Read(string path)
        {
            path += $"\\" + fileName;
            Encoding win1251 = Encoding.GetEncoding(1251);
            var lienes = new List<List<string>>();
            using (var reader = new StreamReader(path, win1251))
            {
                int i = 0;
                while (reader.EndOfStream == false)
                {
                    if (i == 0)
                    {
                        reader.ReadLine();
                        i++;
                        continue;
                    }
                    try
                    {
                        var line = reader.ReadLine();
                        var cells = line.Split(';').ToList();
                        if (HasRowsData(cells))
                        {
                            lienes.Add(cells);
                            Console.WriteLine(line);
                        }


                    }
                    catch
                    {
                        Console.WriteLine("Данные не соответсвуют предполагаемым параметрам");
                    }
                }
            }
            return lienes;
        }
        static bool HasRowsData(List<string> cell)
        {
            return cell.Any(x => x.Length > 0);
        }
        public static List<Code_Name> Get_Code_Names(List<List<string>> list_cells)
        {
            List<Code_Name> code_name = new List<Code_Name>();
            foreach (var cell in list_cells)
            {

                Code_Name new_code = new Code_Name() { Code = cell[1], Name_Process = cell[2] };
                if (code_name.Any(code => code.Code == new_code.Code && code.Name_Process == new_code.Name_Process))
                {
                    continue;
                }
                code_name.Add(new_code);
            }

            return code_name;
        }
        public static List<Code_Category> Get_Code_Category(List<List<string>> list_cells)
        {
            List<Code_Category> code_category = new List<Code_Category>();
            foreach (var cell in list_cells)
            {

                Code_Category new_code = new Code_Category() { Code = cell[1], Category = cell[0] };
                if (code_category.Any(code => code.Code == new_code.Code && code.Category == new_code.Category))
                {
                    continue;
                }
                code_category.Add(new_code);
            }

            return code_category;
        }
        public static List<Name_Division> Get_Name_Division(List<List<string>> list_cells)
        {
            List<Name_Division> code_category = new List<Name_Division>();
            foreach (var cell in list_cells)
            {

                Name_Division new_code = new Name_Division() { Name = cell[2], Division = cell[3] };
                code_category.Add(new_code);
            }
            code_category = code_category.OrderBy(x => x.Name).ToList();
            var r = from p in code_category
                    orderby p.Division descending
                    select p;
            code_category = r.ToList();
            return code_category;
        }
        public static bool WriteDivisionCSV(List<Name_Division> divisions, string path)
        {
            try
            {
                path += "\\Divisions.CSV";
                using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
                {
                    int i = 0;

                    foreach (var division in divisions)
                    {
                        if (i == 0)
                        {
                            string displayNameDivision = PropDisplayName(typeof(Name_Division), nameof(Name_Division.Division));
                            string displayNameName = PropDisplayName(typeof(Name_Division), nameof(Name_Division.Name));
                            writer.WriteLine($"{displayNameDivision};{displayNameName}");
                            i++;
                            continue;
                        }
                        writer.Write($"{division.Division};{division.Name}");

                        writer.WriteLine();
                    }
                    Console.WriteLine($"CSV файл создан (UTF-8,CSV), по пути {path}");
                    return true;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Произошла ошибка, файл открыт в другом процессе!");
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Проихошла ошибка");
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static bool WriteСategoriesCSV(List<Code_Category> categories, string path)
        {
            try
            {
                path += "\\Categories.CSV";
                using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
                {
                    int i = 0;

                    foreach (var category in categories)
                    {
                        if (i == 0)
                        {
                            string displayNameCode = PropDisplayName(typeof(Code_Category), nameof(Code_Category.Code));
                            string displayNameCategory = PropDisplayName(typeof(Code_Category), nameof(Code_Category.Category));
                            writer.WriteLine($"{displayNameCode};{displayNameCategory}");
                            i++;
                            continue;
                        }
                        writer.Write($"{category.Code};{category.Category}");

                        writer.WriteLine();
                    }
                    Console.WriteLine($"CSV файл создан (UTF-8,CSV), по пути {path}");
                    return true;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Произошла ошибка, файл открыт в другом процессе!");
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Проихошла ошибка");
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static bool WriteProcesseseCSV(List<Code_Name> code_names, string path)
        {
            try
            {
                path += "\\Processes.CSV";
                using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
                {
                    int i = 0;

                    foreach (var code_name in code_names)
                    {
                        if (i == 0)
                        {
                            string DisplayNameCode = PropDisplayName(typeof(Code_Name), nameof(Code_Name.Code));
                            string DisplayNameName = PropDisplayName(typeof(Code_Name), nameof(Code_Name.Name_Process));
                            writer.WriteLine($"{DisplayNameCode};{DisplayNameName}");
                            i++;
                            continue;
                        }
                        writer.Write($"{code_name.Code};{code_name.Name_Process}");

                        writer.WriteLine();
                    }
                    Console.WriteLine($"CSV файл создан (UTF-8,CSV), по пути {path}");
                    return true;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Произошла ошибка, файл открыт в другом процессе!");
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Проихошла ошибка");
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        private static string PropDisplayName(Type aType, string aPropName)
        {
            MemberInfo property = aType.GetProperty(aPropName);
            DisplayNameAttribute attribute =
              property.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            return attribute.DisplayName;
        }
    }

}
