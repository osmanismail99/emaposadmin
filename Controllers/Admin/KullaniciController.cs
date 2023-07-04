using EmaPosAdminProject.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmaPosAdminProject.Controllers.Admin
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        EmaDbEntities db = new EmaDbEntities();
        [Authorize]
        public ActionResult KullanicilarListesi()
        {
            var data = db.user.ToList();
            return View(data);
        }

        [Authorize]
        [HttpGet]
        public ActionResult YeniKullanici()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult YeniKullanici(user u)
        {
            db.user.Add(u);
            db.SaveChanges();
            return RedirectToAction("KullanicilarListesi");
        }
        [Authorize]
        public ActionResult KullaniciSil(int id)
        {
            var data = db.user.Find(id);
            db.user.Remove(data);
            db.SaveChanges();
            return RedirectToAction("KullanicilarListesi");
        }
        [Authorize]
        [HttpGet]
        public ActionResult SifreDuzenle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SifreDuzenle(int id, string nowPassword, string newPassword, string newPassword2)
        {
            var data = db.user.Find(id);
            if (data != null && data.password == nowPassword)
            {
                if (newPassword == newPassword2)
                {
                    data.password = newPassword2;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.message = "Yeni Şifre bir birile uyuşmuyor! Lütfen tekrar deneyiniz.";
                    return View();
                }
            }
            else
            {
                ViewBag.message2 = "Mevcut şifreniz hatalıdır! Lütfen tekrar deneyiniz.";
                return View();
            }


        }
    }
}