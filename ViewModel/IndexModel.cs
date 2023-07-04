using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmaPosAdminProject.Models.Entity;

namespace EmaPosAdminProject.ViewModel
{
    public class IndexModel
    {
        public int productCount { get; set; }
        public int newsCount { get; set; }
        public int referenceCount { get; set; }
        public int sectorCount { get; set; }
    }
}