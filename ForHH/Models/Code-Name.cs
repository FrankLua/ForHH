using Aspose.Cells;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ForHH.Models
{
    
    public class Code_Name
    {
        [DisplayName("Код")]
        public string Code { get; set; }
        [DisplayName("Наименование процесса")]
        public string Name_Process { get; set; }
        
    }
    
    public class Code_Category
    {
        [DisplayName("Код процесса")]        
        public string Code { get; set; }
        [DisplayName("Категория процесса")]
        public string Category { get; set; }
    }
    public class Name_Division
    {
        [DisplayName("Наименование процесса")]
        public string Name { get; set; }
        [DisplayName("Подразделение-владелец процесса")]
        public string? Division { get; set; }
    }

}
