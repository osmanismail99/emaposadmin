using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmaPosAdminProject.Models.Entity;

namespace EmaPosAdminProject.Controllers.Admin
{
    public class IletisimController : Controller
    {
        // GET: Iletisim
        EmaDbEntities db = new EmaDbEntities();
        [Authorize]
        public ActionResult IletisimListe()
        {
            var data = db.contact.ToList();
            return View(data);
        }
        [Authorize]
        public ActionResult IletisimGetir(int id)
        {
            var data = db.contact.Find(id);
            return View("IletisimGetir", data);


        }
        [Authorize]
        public ActionResult IletisimGuncelle(contact i)
        {
            var data = db.contact.Find(i.id);
            data.googleMap = i.googleMap;
            data.address = i.address;
            data.phone1 = i.phone1;
            data.phone2 = i.phone2;
            data.email = i.email;
            data.whatsappNumber = i.whatsappNumber;
            data.instagram = i.instagram;
            data.facebook = i.facebook;
            db.SaveChanges();
            return RedirectToAction("IletisimListe");
        }
    }
}