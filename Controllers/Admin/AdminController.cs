using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmaPosAdminProject.Models.Entity;
using EmaPosAdminProject.ViewModel;

namespace EmaPosAdminProject.Controllers.Admin
{
    public class AdminController : Controller
    {
        // GET: Admin


        EmaDbEntities db = new EmaDbEntities();
        [Authorize]
        public ActionResult Index()
        {
            IndexModel model = new IndexModel();
            model.sectorCount = db.sector.Count();
            model.productCount = db.product.Count();
            model.newsCount = db.news.Count();
            model.referenceCount = db.reference.Count();
            return View(model);
        }

        

        


    }
}