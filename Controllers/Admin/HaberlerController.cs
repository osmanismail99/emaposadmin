using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using EmaPosAdminProject.Models.Entity;
using EmaPosAdminProject.ViewModel;

namespace EmaPosAdminProject.Controllers.Admin
{
    public class HaberlerController : Controller
    {
        // GET: Haberler

        EmaDbEntities db = new EmaDbEntities();
        int globalId;
        [Authorize]
        public ActionResult HaberlerListesi()
        {
            NewsImages model = new NewsImages();
            model.NewsList = db.news.ToList();
            model.NewsImageList = db.newsImage.ToList();
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult YeniHaber()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult YeniHaber(NewsImages model)
        {
            if (Request.Files.Count > 0)
            {
                    string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string path = "~/Images/News/" + filename + extension + "";
                    Request.Files[0].SaveAs(Server.MapPath(path));
                    model.NewsImage.image = filename + extension;
            }
            model.NewsImage.newsId = model.News.id;
            model.News.date = DateTime.Now;
            db.news.Add(model.News);
            db.newsImage.Add(model.NewsImage);
            db.SaveChanges();
            return RedirectToAction("HaberlerListesi");
        }
        [Authorize]
        public ActionResult HaberSil(int id)
        {
            var data = db.news.Find(id);
            List<newsImage> newsList = db.newsImage.Where(x => x.newsId == id).ToList();
            foreach (var item in newsList)
            {
                db.newsImage.Remove(item);
            }
            db.news.Remove(data);
            db.SaveChanges();
            return RedirectToAction("HaberlerListesi");
        }
        [Authorize]
        public ActionResult HaberGetir(int id)
        {

            var data = db.news.Find(id);
            return View("HaberGetir", data);
        }
        [Authorize]
        public ActionResult HaberGuncelle(news n)
        {
            var data = db.news.Find(n.id);
            data.title = n.title;
            data.text = n.text;
            db.SaveChanges();
            return RedirectToAction("HaberlerListesi");
        }

        [Authorize]
        public ActionResult GorsellerListesi(int id)
        {
            NewsImages model = new NewsImages();
            model.NewsList = db.news.Where(x => x.id == id).ToList();
            model.NewsImageList = db.newsImage.Where(x => x.newsId == id).ToList();
            model.Id = id;
            globalId = id;
            return View(model);
        }



        [Authorize]
        [HttpPost]
        public ActionResult YeniGorsel(NewsImages model)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/News/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                model.NewsImage.image = filename + extension;
            }
            model.NewsImage.newsId = model.Id;
            db.newsImage.Add(model.NewsImage);
            db.SaveChanges();
            return RedirectPermanent("/Haberler/GorsellerListesi/" + model.Id);
        }
        [Authorize]
        public ActionResult GorselSil(int id)
        {
            var data = db.newsImage.Find(id);
            int myId = (int)data.newsId;
            db.newsImage.Remove(data);
            db.SaveChanges();
            return RedirectPermanent("/Haberler/GorsellerListesi/" + myId);
        }
        [Authorize]
        public ActionResult GorselGetir(int id)
        {
            var data = db.newsImage.Find(id);
            return View("GorselGetir", data);
        }
        [Authorize]
        public ActionResult GorselGuncelle(newsImage ni)
        {
            if (Request.Files.Count > 0 && ni.image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/News/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                ni.image = filename + extension;
            }
            var data = db.newsImage.Find(ni.id);
            int myId = (int)data.newsId;
            if (ni.image != null)
            {
                data.image = ni.image;
            }
            db.SaveChanges();
            return RedirectPermanent("/Haberler/GorsellerListesi/" + myId);
        }

    }
}