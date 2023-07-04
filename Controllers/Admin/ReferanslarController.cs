using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmaPosAdminProject.Models.Entity;

namespace EmaPosAdminProject.Controllers.Admin
{
    public class ReferanslarController : Controller
    {
        // GET: Referanslar
        EmaDbEntities db = new EmaDbEntities();
        [Authorize]
        public ActionResult ReferanslarListesi()
        {
            var data = db.reference.ToList();
            return View(data);
        }
        [Authorize]
        [HttpGet]
        public ActionResult YeniReferans()
        {
            if (db.reference.Count() > 0)
            {
                ViewBag.orderNum = db.reference.Max(x => x.orderNumber).Value;
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
        public ActionResult YeniReferans(reference r)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/References/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                r.image = filename + extension;
            }
            db.reference.Add(r);
            db.SaveChanges();
            return RedirectToAction("ReferanslarListesi");
        }
        [Authorize]
        public ActionResult ReferansSil(int id)
        {
            var data = db.reference.Find(id);
            db.reference.Remove(data);
            db.SaveChanges();
            return RedirectToAction("ReferanslarListesi");
        }
        [Authorize]
        public ActionResult ReferansGetir(int id)
        {
            var data = db.reference.Find(id);
            return View("ReferansGetir", data);
        }
        [Authorize]
        public ActionResult ReferansGuncelle(reference r)
        {
            if (Request.Files.Count > 0 && r.image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/References/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                r.image = filename + extension;
            }
            var data = db.reference.Find(r.id);
            data.orderNumber = r.orderNumber;
            data.title = r.title;
            data.facebook = r.facebook;
            data.instagram = r.instagram;
            data.website = r.website;
            if (r.image != null)
            {
                data.image = r.image;
            }
            db.SaveChanges();
            return RedirectToAction("ReferanslarListesi");
        }
    }
}