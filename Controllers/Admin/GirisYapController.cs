using EmaPosAdminProject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EmaPosAdminProject.ViewModel;

namespace EmaPosAdminProject.Controllers.Admin
{
    public class GirisYapController : Controller
    {
        // GET: GirisYap
        EmaDbEntities db = new EmaDbEntities();
        public static user nowUser { get; set; }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user u)
        {
            var data = db.user.FirstOrDefault(x => x.username == u.username && x.password == u.password);
            if (data != null)
            {
                FormsAuthentication.SetAuthCookie(data.username, false);
                Session["username"] = data.username.ToString();
                nowUser = data;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.error = "Kullanıcı adınız veya şifreniz HATALIDIR!";
                return View();
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "GirisYap");
        }
    }
}