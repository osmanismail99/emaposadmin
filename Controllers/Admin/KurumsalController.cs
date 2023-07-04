using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmaPosAdminProject.Models.Entity;

namespace EmaPosAdminProject.Controllers.Admin
{
    public class KurumsalController : Controller
    {
        // GET: Kurumsal
        EmaDbEntities db = new EmaDbEntities();
        [Authorize]
        public ActionResult HakkimizdaListe()
        {
            var data = db.about.ToList();
            return View(data);
        }

        [Authorize]
        public ActionResult HakkimizdaGetir(int id)
        {
            var data = db.about.Find(id);
            return View("HakkimizdaGetir", data);
        }

        [Authorize]
        public ActionResult HakkimizdaGuncelle(about a)
        {
            if (Request.Files.Count > 0 && a.image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Kurumsal/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                a.image = filename + extension;
            }
            var data = db.about.Find(a.id);
            data.text = a.text;
            if (a.image != null)
            {
                data.image = a.image;
            }
            db.SaveChanges();
            return RedirectToAction("HakkimizdaListe");
        }

        [Authorize]
        public ActionResult MisyonumuzListe()
        {
            var data = db.mission.ToList();
            return View(data);
        }

        [Authorize]
        public ActionResult MisyonumuzGetir(int id)
        {
            var data = db.mission.Find(id);
            return View("MisyonumuzGetir", data);
        }

        [Authorize]
        public ActionResult MisyonumuzGuncelle(mission m)
        {
            if (Request.Files.Count > 0 && m.image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Kurumsal/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                m.image = filename + extension;
            }
            var data = db.mission.Find(m.id);
            data.text = m.text;
            if (m.image != null)
            {
                data.image = m.image;
            }
            db.SaveChanges();
            return RedirectToAction("MisyonumuzListe");
        }

        [Authorize]
        public ActionResult VizyonumuzListe()
        {
            var data = db.vision.ToList();
            return View(data);
        }

        [Authorize]
        public ActionResult VizyonumuzGetir(int id)
        {
            var data = db.vision.Find(id);
            return View("VizyonumuzGetir", data);
        }

        [Authorize]
        public ActionResult VizyonumuzGuncelle(vision v)
        {
            if (Request.Files.Count > 0 && v.image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Kurumsal/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                v.image = filename + extension;
            }
            var data = db.vision.Find(v.id);
            data.text = v.text;
            if (v.image != null)
            {
                data.image = v.image;
            }
            db.SaveChanges();
            return RedirectToAction("VizyonumuzListe");
        }

    }
}