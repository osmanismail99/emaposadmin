using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmaPosAdminProject.Models.Entity;

namespace EmaPosAdminProject.Controllers.Admin
{
    public class KategoriController : Controller
    {
        EmaDbEntities db = new EmaDbEntities();
        // GET: Kategori
        [Authorize]
        public ActionResult KategoriListesi()
        {
            var data = db.category.ToList();
            return View(data);
        }
        [Authorize]
        [HttpGet]
        public ActionResult YeniKategori()
        {
            if (db.category.Count() > 0)
            {
                ViewBag.orderNum = db.category.Max(x => x.orderNumber).Value;
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
        public ActionResult YeniKategori(category c)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Category/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                c.image = filename + extension;
            }
            db.category.Add(c);
            db.SaveChanges();
            return RedirectToAction("KategoriListesi");
        }
        [Authorize]
        public ActionResult KategoriSil(int id)
        {
            var data = db.category.Find(id);
            List<product> productsList = db.product.Where(x => x.categoryId == id).ToList();
            foreach (var item in productsList)
            {
                db.product.Remove(item);
                List<productImage> images = db.productImage.Where(x => x.productId == item.id).ToList();
                foreach (var image in images)
                {
                    db.productImage.Remove(image);
                }
               
            }
            List<sectorsCategory> sectorsCategories = db.sectorsCategory.Where(x => x.categoryId == id).ToList();
            foreach (var x in sectorsCategories)
            {
                db.sectorsCategory.Remove(x);
            }
            db.category.Remove(data);
            db.SaveChanges();
            return RedirectToAction("KategoriListesi");
        }
        [Authorize]
        public ActionResult KategoriGetir(int id)
        {
            var data = db.category.Find(id);
            return View("KategoriGetir", data);

        }
        [Authorize]
        public ActionResult KategoriGuncelle(category c)
        {
            if (Request.Files.Count > 0 && c.image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Category/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                c.image = filename + extension;
            }
            var data = db.category.Find(c.id);
            data.name = c.name;
            data.orderNumber= c.orderNumber;
            if (c.image != null)
            {
                data.image = c.image;
            }
            db.SaveChanges();
            return RedirectToAction("KategoriListesi");
        }


    }
}