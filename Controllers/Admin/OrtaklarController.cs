using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmaPosAdminProject.Models.Entity;

namespace EmaPosAdminProject.Controllers.Admin
{
    public class OrtaklarController : Controller
    {
        // GET: Ortaklar
        EmaDbEntities db = new EmaDbEntities();
        [Authorize]
        public ActionResult OrtaklarListesi()
        {

            var data = db.solutionPartners.ToList();
            return View(data);
        }

        [Authorize]
        [HttpGet]
        public ActionResult YeniOrtak()
        {
            if (db.solutionPartners.Count() > 0)
            {
                ViewBag.orderNum = db.solutionPartners.Max(x => x.orderNumber).Value;
                ViewBag.orderNum++;
            }
            else
            {
                ViewBag.orderNum = 1;
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult YeniOrtak(solutionPartners sp)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/SolutionPartners/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                sp.image = filename + extension;
            }           
            db.solutionPartners.Add(sp);
            db.SaveChanges();
            return RedirectToAction("OrtaklarListesi");
        }
        [Authorize]
        public ActionResult OrtakSil(int id)
        {
            var data = db.solutionPartners.Find(id);
            db.solutionPartners.Remove(data);
            db.SaveChanges();
            return RedirectToAction("OrtaklarListesi");
        }
        [Authorize]
        public ActionResult OrtakGetir(int id)
        {
            var data = db.solutionPartners.Find(id);
            return View("OrtakGetir", data);
        }
        [Authorize]
        public ActionResult OrtakGuncelle(solutionPartners sp)
        {
            if (Request.Files.Count > 0 && sp.image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/SolutionPartners/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                sp.image = filename + extension;
            }
            var data = db.solutionPartners.Find(sp.id);
            data.orderNumber = sp.orderNumber;
            data.name = sp.name;
            data.slogan = sp.slogan;
            if (sp.image != null)
            {
                data.image = sp.image;
            }
            db.SaveChanges();
            return RedirectToAction("OrtaklarListesi");
        }
    }
}