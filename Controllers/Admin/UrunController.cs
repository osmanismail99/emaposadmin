using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using EmaPosAdminProject.Models.Entity;
using EmaPosAdminProject.ViewModel;

namespace EmaPosAdminProject.Controllers.Admin
{
    public class UrunController : Controller
    {
        // GET: Urun
        EmaDbEntities db = new EmaDbEntities();
        [Authorize]
        public ActionResult UrunlerListesi()
        {
            ProductImages model = new ProductImages();
            model.ProductList = db.product.ToList();
            //var categoryid=model.ProductList.Select(x=>x.categoryId).FirstOrDefault();
            model.ProductImageList = db.productImage.ToList();
            //model.myCategory = (from c in db.category  where c.id == categoryid select c.category1.FirstOrDefault()).ToString();
          
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public ActionResult YeniUrun()
        {
            ProductImages model = new ProductImages();
            model.CategoryList = db.category.ToList();
            ViewBag.category = db.category.ToList();
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult YeniUrun(ProductImages model,int id)
        {
            //if (Request.Files.Count > 0)
            //{
            //    string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
            //    string extension = Path.GetExtension(Request.Files[0].FileName);
            //    string path = "~/Images/Category" + filename + extension + "";
            //    Request.Files[0].SaveAs(Server.MapPath(path));
            //    model.ProductImage.image = filename + extension;
            //}
            //model.ProductImage.productId = model.Product.id;
            model.Product.categoryId = id;
            db.product.Add(model.Product);
            //db.productImage.Add(model.ProductImage);
            db.SaveChanges();
            return RedirectToAction("UrunlerListesi");
        }
        [Authorize]
        public ActionResult UrunSil(int id)
        {
            var data = db.product.Find(id);
            List<productImage> imageList = db.productImage.Where(x => x.productId == id).ToList();
            foreach (var item in imageList)
            {
                db.productImage.Remove(item);
            }
            db.product.Remove(data);
            db.SaveChanges();
            return RedirectToAction("UrunlerListesi");
        }
        [Authorize]
        public ActionResult UrunGetir(int id)
        {
            ProductImages model = new ProductImages();
            model.Product = db.product.Find(id);
            model.CategoryList = db.category.ToList();
            ViewBag.category = db.category.ToList();
            return View("UrunGetir", model);
        }
        [Authorize]
        public ActionResult UrunGuncelle(ProductImages p,int id)
        {
            var data = db.product.Find(p.Product.id);
            data.title = p.Product.title;
            data.text = p.Product.text;
            data.categoryId = id;
            db.SaveChanges();
            return RedirectToAction("UrunlerListesi");
        }
        [Authorize]
        public ActionResult GorsellerListesi(int id)
        {
            ProductImages model = new ProductImages();
            model.ProductList = db.product.Where(x => x.id == id).ToList();
            model.ProductImageList = db.productImage.Where(x => x.productId == id).ToList();
            model.Id = id;
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult YeniGorsel(ProductImages model)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Category/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                model.ProductImage.image = filename + extension;
            }
            model.ProductImage.productId = model.Id;
            db.productImage.Add(model.ProductImage);
            db.SaveChanges();
            return RedirectPermanent("/Urun/GorsellerListesi/" + model.Id);
        }
        [Authorize]
        public ActionResult GorselSil(int id)
        {
            var data = db.productImage.Find(id);
            int myId = (int)data.productId;
            db.productImage.Remove(data);
            db.SaveChanges();
            return RedirectPermanent("/Urun/GorsellerListesi/" + myId);
        }
        [Authorize]
        public ActionResult GorselGetir(int id)
        {
            var data = db.productImage.Find(id);
            return View("GorselGetir", data);
        }
        [Authorize]
        public ActionResult GorselGuncelle(productImage pi)
        {
            if (Request.Files.Count > 0 && pi.image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Category/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                pi.image = filename + extension;
            }
            var data = db.productImage.Find(pi.id);
            int myId = (int)data.productId;
            if (pi.image != null)
            {
                data.image = pi.image;
            }
            db.SaveChanges();
            return RedirectPermanent("/Urun/GorsellerListesi/" + myId);
        }
    }
}