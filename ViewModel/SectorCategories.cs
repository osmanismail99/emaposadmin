using EmaPosAdminProject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmaPosAdminProject.ViewModel
{
    public class SectorCategories
    {
        public  int Id { get; set; }
        public List<sector> SectortList { get; set; }
        public List<sectorsCategory> SectorsCategoryList { get; set; }
        public List<category> CategoryList { get; set; }

        public sector Sector { get; set; }
        public sectorsCategory SectorsCategory { get; set; }
        public category Category { get; set; }

        public string myCategory { get; set; }
    }
}