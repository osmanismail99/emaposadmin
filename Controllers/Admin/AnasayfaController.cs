using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmaPosAdminProject.Models.Entity;

namespace EmaPosAdminProject.Controllers.Admin
{
    public class AnasayfaController : Controller
    {
        // GET: Anasayfa

        EmaDbEntities db = new EmaDbEntities();

        [Authorize]
        public ActionResult SliderListe()
        {
            var data = db.indexCarousel.ToList();
            return View(data);
        }

        [Authorize]
        [HttpGet]
        public ActionResult YeniSlider()
        {
            if (db.indexCarousel.Count() > 0)
            {
                ViewBag.orderNum = db.indexCarousel.Max(x => x.orderNumber).Value;
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
        public ActionResult YeniSlider(indexCarousel ic)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Slider/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                ic.image = filename + extension;
            }
            if (db.indexCarousel.Count() > 0 && ic.orderNumber > db.indexCarousel.Max(x => x.orderNumber).Value)
            {
                ic.orderNumber = db.indexCarousel.Max(x => x.orderNumber);
                ic.orderNumber++;
            }
            else
            {
                ic.orderNumber = 1;
            }
            db.indexCarousel.Add(ic);
            db.SaveChanges();
            return RedirectToAction("SliderListe");
        }

        [Authorize]
        public ActionResult SliderSil(int id)
        {
            var data = db.indexCarousel.Find(id);
            db.indexCarousel.Remove(data);
            db.SaveChanges();
            return RedirectToAction("SliderListe");
        }

        [Authorize]
        public ActionResult SliderGetir(int id)
        {
            var data = db.indexCarousel.Find(id);
            return View("SliderGetir", data);
        }

        [Authorize]
        public ActionResult SliderGuncelle(indexCarousel ic)
        {
            if (Request.Files.Count > 0 && ic.image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Slider/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                ic.image = filename + extension;
            }
            var data = db.indexCarousel.Find(ic.id);
            data.orderNumber = ic.orderNumber;
            data.title = ic.title;
            if (ic.image != null)
            {
                data.image = ic.image;
            }
            db.SaveChanges();
            return RedirectToAction("SliderListe");
        }

        [Authorize]
        public ActionResult BizKimizListe()
        {
            var data = db.indexWhoAreWe.ToList();
            return View(data);
        }

        [Authorize]
        public ActionResult BizKimizGetir(int id)
        {
            var data = db.indexWhoAreWe.Find(id);
            return View("BizKimizGetir", data);
        }

        [Authorize]
        public ActionResult BizKimizGuncelle(indexWhoAreWe iwaw)
        {
            if (Request.Files.Count > 0 && iwaw.image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/WhoAreWe/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                iwaw.image = filename + extension;
            }
            var data = db.indexWhoAreWe.Find(iwaw.id);
            data.title = iwaw.title;
            data.shortText = iwaw.shortText;
            data.text = iwaw.text;
            if (iwaw.image != null)
            {
                data.image = iwaw.image;
            }
            db.SaveChanges();
            return RedirectToAction("BizKimizListe");
        }

        [Authorize]
        public ActionResult BizeUlasinListe()
        {
            var data = db.indexContactUs.ToList();
            return View(data);
        }

        [Authorize]
        public ActionResult BizeUlasinGetir(int id)
        {
            var data = db.indexContactUs.Find(id);
            return View("BizeUlasinGetir", data);
        }

        [Authorize]
        public ActionResult BizeUlasinGuncelle(indexContactUs icu)
        {
            if (Request.Files.Count > 0 && icu.image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/indexContactUs/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                icu.image = filename + extension;
            }
            var data = db.indexContactUs.Find(icu.id);
            data.name = icu.name;
            data.slogan = icu.slogan;
            data.text = icu.text;
            if (icu.image != null)
            {
                data.image = icu.image;
            }
            db.SaveChanges();
            return RedirectToAction("BizeUlasinListe");
        }

        [Authorize]
        public ActionResult UrunlerListesi()
        {
            var data = db.indexProducts.ToList();
            return View(data);
        }

        [Authorize]
        public ActionResult UrunDegis(int id)
        {
            var data = db.indexProducts.Find(id);
            if (data.visibility == true)
            { data.visibility = false; }
            else { data.visibility = true; }
            db.SaveChanges();
            return RedirectToAction("UrunlerListesi");
        }

        [Authorize]
        public ActionResult UrunGetir(int id)
        {
            var data = db.indexProducts.Find(id);
            return View("UrunGetir", data);
        }

        [Authorize]
        public ActionResult UrunGuncelle(indexProducts ip)
        {
            if (Request.Files.Count > 0 && ip.image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Category/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                ip.image = filename + extension;
            }
            var data = db.indexProducts.Find(ip.id);
            data.title = ip.title;
            if (ip.image != null)
            {
                data.image = ip.image;
            }
            db.SaveChanges();
            return RedirectToAction("UrunlerListesi");
        }

    }
}