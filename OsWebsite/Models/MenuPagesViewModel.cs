using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OsWebsite.Models
{
    public class MenuPagesViewModel
    {
        public int IdMenuPage { get; set; }
        public string Name { get; set; }
        public string CodeMenu { get; set; }
        public bool IsActive { get; set; }
        public int IDMenuType { get; set; }
        public int MenuPageID { get; set; }
        public int MenuID { get; set; }
        public int? IDMenu { get; set; }
        public string NameMenu { get; set; }
        
    }
}